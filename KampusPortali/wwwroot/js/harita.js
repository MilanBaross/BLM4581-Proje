(function () {
    const haritaKutusu = document.getElementById('kampusHaritasi');
    if (!haritaKutusu || typeof L === 'undefined') {
        return;
    }

    const mekanlar = JSON.parse(document.getElementById('mekanVerisi').textContent);
    const stil = getComputedStyle(document.documentElement);
    const vurgu = stil.getPropertyValue('--vurgu').trim() || '#0e4a87';
    const vurguKoyu = stil.getPropertyValue('--vurgu-koyu').trim() || '#0a3866';
    const etiketYakinligi = 17;

    const harita = L.map(haritaKutusu).setView([39.7805, 32.8200], 16);

    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> katkıda bulunanlar'
    }).addTo(harita);

    fetch('/data/kampus_diger_binalar.geojson')
        .then(function (yanit) { return yanit.ok ? yanit.json() : null; })
        .then(function (veri) {
            if (!veri) {
                return;
            }
            L.geoJSON(veri, {
                interactive: false,
                style: { color: '#9aa3ae', weight: 1, fillColor: '#c5ccd6', fillOpacity: 0.6 }
            }).addTo(harita).bringToBack();
        })
        .catch(function () { });

    function binaStili(tur) {
        const doluluk = tur === 'Fakulte' ? 0.65 : tur === 'Yurt' ? 0.35 : 0.5;
        return { color: vurguKoyu, weight: 1, fillColor: vurgu, fillOpacity: doluluk };
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

    const katmanlar = new Map();
    let vurgulanan = null;

    mekanlar.forEach(function (mekan) {
        let katman;

        if (mekan.geometri) {
            katman = L.geoJSON(mekan.geometri, { style: binaStili(mekan.tur) });
            katman.bindTooltip(mekan.ad, {
                permanent: true,
                direction: 'center',
                className: 'bina-etiket'
            });
        } else {
            const ikon = L.divIcon({
                className: 'harita-isaret-kapsayici',
                html: '<span class="harita-isaret"><i class="bi ' + mekan.ikon + '"></i></span>',
                iconSize: [32, 32],
                iconAnchor: [16, 32],
                popupAnchor: [0, -30]
            });
            katman = L.marker([mekan.enlem, mekan.boylam], { icon: ikon, title: mekan.ad });
        }

        katman.bindPopup(popupOlustur(mekan)).addTo(harita);
        katmanlar.set(mekan.id, { katman: katman, tur: mekan.tur, bina: !!mekan.geometri });
    });

    function vurgula(kayit) {
        if (vurgulanan && vurgulanan.bina) {
            vurgulanan.katman.setStyle(binaStili(vurgulanan.tur));
        }
        if (kayit.bina) {
            kayit.katman.setStyle({ weight: 3, fillOpacity: 0.9 });
            kayit.katman.bringToFront();
        }
        vurgulanan = kayit;
    }

    function etiketleriGuncelle() {
        haritaKutusu.classList.toggle('etiketler-gizli', harita.getZoom() < etiketYakinligi);
    }

    harita.on('zoomend', etiketleriGuncelle);

    if (mekanlar.length > 0) {
        const sinirlar = L.latLngBounds([]);
        katmanlar.forEach(function (kayit) {
            if (kayit.bina) {
                sinirlar.extend(kayit.katman.getBounds());
            } else {
                sinirlar.extend(kayit.katman.getLatLng());
            }
        });
        harita.fitBounds(sinirlar, { padding: [30, 30], maxZoom: 17 });
    }

    etiketleriGuncelle();

    document.querySelectorAll('.filtre-dugme').forEach(function (dugme) {
        dugme.addEventListener('click', function () {
            const secilenTur = dugme.dataset.tur;

            document.querySelectorAll('.filtre-dugme').forEach(function (d) {
                d.classList.toggle('aktif', d === dugme);
            });

            katmanlar.forEach(function (kayit) {
                const gorunsun = !secilenTur || kayit.tur === secilenTur;
                if (gorunsun) {
                    kayit.katman.addTo(harita);
                } else {
                    kayit.katman.remove();
                }
            });

            document.querySelectorAll('.mekan-listesi li').forEach(function (satir) {
                satir.hidden = !!secilenTur && satir.dataset.tur !== secilenTur;
            });
        });
    });

    document.querySelectorAll('.mekan-ogesi').forEach(function (oge) {
        oge.addEventListener('click', function () {
            const kayit = katmanlar.get(Number(oge.dataset.id));
            if (!kayit) {
                return;
            }

            if (kayit.bina) {
                harita.flyToBounds(kayit.katman.getBounds(), { padding: [60, 60], maxZoom: 18 });
            } else {
                harita.flyTo(kayit.katman.getLatLng(), 18);
            }

            vurgula(kayit);
            kayit.katman.openPopup();

            if (window.innerWidth < 992) {
                haritaKutusu.scrollIntoView({ behavior: 'smooth', block: 'center' });
            }
        });
    });
})();
