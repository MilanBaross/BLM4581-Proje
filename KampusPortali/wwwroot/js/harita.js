(function () {
    const haritaKutusu = document.getElementById('kampusHaritasi');
    if (!haritaKutusu || typeof L === 'undefined') {
        return;
    }

    const mekanlar = JSON.parse(document.getElementById('mekanVerisi').textContent);
    const stil = getComputedStyle(document.documentElement);
    const vurgu = stil.getPropertyValue('--vurgu').trim() || '#0e4a87';
    const vurguKoyu = stil.getPropertyValue('--vurgu-koyu').trim() || '#0a3866';

    const METRE_ENLEM = 1 / 111320;
    const KUZEY_EGIM = 0.9;
    const DOGU_EGIM = 0.35;
    const etiketYakinligi = 17;

    const turRenkleri = {
        Fakulte: { duvar: vurgu, cati: '#2f6aa8', yukseklik: 14 },
        Yurt: { duvar: '#5c86b5', cati: '#8fb0d4', yukseklik: 18 },
        Kantin: { duvar: '#2f6aa8', cati: '#4a80b8', yukseklik: 5 },
        Diger: { duvar: '#2f6aa8', cati: '#4a80b8', yukseklik: 10 },
        Gri: { duvar: '#aeb6c1', cati: '#d3d9e1', yukseklik: 8 }
    };

    const harita = L.map(haritaKutusu, {
        minZoom: 13,
        maxZoom: 19,
        renderer: L.svg({ padding: 1.5 })
    }).setView([39.7805, 32.8200], 16);

    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> katkıda bulunanlar'
    }).addTo(harita);

    function renkKoyulastir(renk, oran) {
        const deger = renk.replace('#', '');
        const parca = function (i) {
            return Math.round(parseInt(deger.substr(i, 2), 16) * oran).toString(16).padStart(2, '0');
        };
        return '#' + parca(0) + parca(2) + parca(4);
    }

    function kaydir(nokta, yukseklik) {
        const metreBoylam = METRE_ENLEM / Math.cos(nokta[0] * Math.PI / 180);
        return [
            nokta[0] + yukseklik * KUZEY_EGIM * METRE_ENLEM,
            nokta[1] + yukseklik * DOGU_EGIM * metreBoylam
        ];
    }

    function poligonlar(geometri) {
        const parcalar = geometri.type === 'Polygon' ? [geometri.coordinates] : geometri.coordinates;
        return parcalar.map(function (halkalar) {
            return halkalar.map(function (halka) {
                return halka.map(function (k) { return [k[1], k[0]]; });
            });
        });
    }

    function binaParcalari(geometri, yukseklik) {
        return poligonlar(geometri).map(function (halkalar) {
            const duvarlar = [];

            halkalar.forEach(function (halka) {
                for (let i = 0; i < halka.length - 1; i++) {
                    const a = halka[i];
                    const b = halka[i + 1];
                    const dx = b[1] - a[1];
                    const dy = b[0] - a[0];
                    const uzunluk = Math.hypot(dx, dy) || 1;
                    duvarlar.push({
                        koseler: [a, b, kaydir(b, yukseklik), kaydir(a, yukseklik)],
                        sira: (a[0] + b[0]) / 2,
                        aydinlik: 0.55 + 0.35 * Math.abs(dx) / uzunluk
                    });
                }
            });

            duvarlar.sort(function (x, y) { return y.sira - x.sira; });

            return {
                duvarlar: duvarlar,
                cati: halkalar.map(function (halka) {
                    return halka.map(function (n) { return kaydir(n, yukseklik); });
                }),
                enGuney: Math.min.apply(null, halkalar[0].map(function (n) { return n[0]; }))
            };
        });
    }

    function popupOlustur(mekan) {
        const kutu = document.createElement('div');

        const baslik = document.createElement('div');
        baslik.className = 'harita-popup-baslik';
        baslik.textContent = mekan.ad;
        kutu.appendChild(baslik);

        const tur = document.createElement('div');
        tur.className = 'harita-popup-tur';
        tur.textContent = mekan.turAdi;
        kutu.appendChild(tur);

        if (mekan.aciklama) {
            const aciklama = document.createElement('p');
            aciklama.className = 'harita-popup-aciklama';
            aciklama.textContent = mekan.aciklama;
            kutu.appendChild(aciklama);
        }

        return kutu;
    }

    const binaKatmani = L.layerGroup().addTo(harita);
    const isaretKatmani = L.layerGroup().addTo(harita);
    const etiketKatmani = L.layerGroup().addTo(harita);

    const kayitlar = new Map();
    let digerBinalar = [];
    let secilenTur = '';
    let seciliKayit = null;

    mekanlar.forEach(function (mekan) {
        const kayit = { mekan: mekan, sinir: null, catilar: [], etiketKonumu: null };

        if (mekan.geometri) {
            const yukseklik = (turRenkleri[mekan.tur] || turRenkleri.Diger).yukseklik;
            kayit.sinir = L.geoJSON(mekan.geometri).getBounds();
            kayit.etiketKonumu = kaydir([mekan.enlem, mekan.boylam], yukseklik);
        } else {
            kayit.isaret = L.marker([mekan.enlem, mekan.boylam], {
                title: mekan.ad,
                icon: L.divIcon({
                    className: 'harita-isaret-kapsayici',
                    html: '<span class="harita-isaret"><i class="bi ' + mekan.ikon + '"></i></span>',
                    iconSize: [32, 32],
                    iconAnchor: [16, 32],
                    popupAnchor: [0, -30]
                })
            }).bindPopup(popupOlustur(mekan));
        }

        kayitlar.set(mekan.id, kayit);
    });

    function turGorunurMu(tur) {
        return !secilenTur || tur === secilenTur;
    }

    function ciz() {
        binaKatmani.clearLayers();
        isaretKatmani.clearLayers();
        etiketKatmani.clearLayers();
        seciliKayit = null;

        const parcalar = [];

        kayitlar.forEach(function (kayit) {
            kayit.catilar = [];
            const mekan = kayit.mekan;
            if (!turGorunurMu(mekan.tur)) {
                return;
            }
            if (kayit.isaret) {
                isaretKatmani.addLayer(kayit.isaret);
                return;
            }
            const renk = turRenkleri[mekan.tur] || turRenkleri.Diger;
            binaParcalari(mekan.geometri, renk.yukseklik).forEach(function (parca) {
                parcalar.push({ parca: parca, renk: renk, kayit: kayit });
            });

            const yazi = document.createElement('span');
            yazi.textContent = mekan.ad;
            L.marker(kayit.etiketKonumu, {
                interactive: false,
                keyboard: false,
                icon: L.divIcon({ className: 'bina-etiket', html: yazi, iconSize: [110, 30], iconAnchor: [55, 15] })
            }).addTo(etiketKatmani);
        });

        if (!secilenTur) {
            digerBinalar.forEach(function (ozellik) {
                binaParcalari(ozellik.geometry, turRenkleri.Gri.yukseklik).forEach(function (parca) {
                    parcalar.push({ parca: parca, renk: turRenkleri.Gri, kayit: null });
                });
            });
        }

        parcalar.sort(function (x, y) { return y.parca.enGuney - x.parca.enGuney; });

        parcalar.forEach(function (oge) {
            const tiklanir = !!oge.kayit;
            const kenar = renkKoyulastir(oge.renk.duvar, 0.55);

            oge.parca.duvarlar.forEach(function (duvar) {
                const sekil = L.polygon(duvar.koseler, {
                    interactive: tiklanir,
                    color: kenar,
                    weight: 0.5,
                    fillColor: renkKoyulastir(oge.renk.duvar, duvar.aydinlik),
                    fillOpacity: 1
                }).addTo(binaKatmani);
                if (tiklanir) {
                    sekil.on('click', function () { binaSec(oge.kayit, false); });
                }
            });

            const cati = L.polygon(oge.parca.cati, {
                interactive: tiklanir,
                color: kenar,
                weight: 1,
                fillColor: oge.renk.cati,
                fillOpacity: 1
            }).addTo(binaKatmani);

            if (tiklanir) {
                cati.on('click', function () { binaSec(oge.kayit, false); });
                oge.kayit.catilar.push({ sekil: cati, renk: oge.renk.cati });
            }
        });
    }

    function binaSec(kayit, uc) {
        if (seciliKayit) {
            seciliKayit.catilar.forEach(function (c) { c.sekil.setStyle({ fillColor: c.renk, weight: 1 }); });
        }
        kayit.catilar.forEach(function (c) { c.sekil.setStyle({ fillColor: '#f2c94c', weight: 2 }); });
        seciliKayit = kayit;

        const ac = function () {
            L.popup({ offset: [0, -4] })
                .setLatLng(kayit.etiketKonumu)
                .setContent(popupOlustur(kayit.mekan))
                .openOn(harita);
        };

        if (uc) {
            harita.flyToBounds(kayit.sinir, { padding: [80, 80], maxZoom: 18 });
            harita.once('moveend', ac);
        } else {
            ac();
        }
    }

    harita.on('popupclose', function () {
        if (seciliKayit) {
            seciliKayit.catilar.forEach(function (c) { c.sekil.setStyle({ fillColor: c.renk, weight: 1 }); });
            seciliKayit = null;
        }
    });

    function etiketleriGuncelle() {
        haritaKutusu.classList.toggle('etiketler-gizli', harita.getZoom() < etiketYakinligi);
    }

    harita.on('zoomend', etiketleriGuncelle);

    const tumSinirlar = L.latLngBounds([]);
    kayitlar.forEach(function (kayit) {
        tumSinirlar.extend(kayit.sinir || [kayit.mekan.enlem, kayit.mekan.boylam]);
    });
    if (tumSinirlar.isValid()) {
        harita.fitBounds(tumSinirlar, { padding: [20, 20], maxZoom: 17 });
    }

    ciz();
    etiketleriGuncelle();

    fetch('/data/kampus_diger_binalar.geojson')
        .then(function (yanit) { return yanit.ok ? yanit.json() : null; })
        .then(function (veri) {
            if (veri) {
                digerBinalar = veri.features;
                ciz();
            }
        })
        .catch(function () { });

    document.querySelectorAll('.filtre-dugme').forEach(function (dugme) {
        dugme.addEventListener('click', function () {
            secilenTur = dugme.dataset.tur;

            document.querySelectorAll('.filtre-dugme').forEach(function (d) {
                d.classList.toggle('aktif', d === dugme);
            });

            harita.closePopup();
            ciz();

            document.querySelectorAll('.mekan-listesi li').forEach(function (satir) {
                satir.hidden = !!secilenTur && satir.dataset.tur !== secilenTur;
            });
        });
    });

    document.querySelectorAll('.mekan-ogesi').forEach(function (oge) {
        oge.addEventListener('click', function () {
            const kayit = kayitlar.get(Number(oge.dataset.id));
            if (!kayit) {
                return;
            }

            if (kayit.sinir) {
                binaSec(kayit, true);
            } else {
                harita.flyTo([kayit.mekan.enlem, kayit.mekan.boylam], 18);
                harita.once('moveend', function () { kayit.isaret.openPopup(); });
            }

            if (window.innerWidth < 992) {
                haritaKutusu.scrollIntoView({ behavior: 'smooth', block: 'center' });
            }
        });
    });
})();
