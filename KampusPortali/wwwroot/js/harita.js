(function () {
    const haritaKutusu = document.getElementById('kampusHaritasi');
    if (!haritaKutusu || typeof L === 'undefined') {
        return;
    }

    const mekanlar = JSON.parse(document.getElementById('mekanVerisi').textContent);
    const varsayilanMerkez = [39.7860, 32.8140];

    const harita = L.map(haritaKutusu).setView(varsayilanMerkez, 15);

    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> katkıda bulunanlar'
    }).addTo(harita);

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

    const isaretler = new Map();

    mekanlar.forEach(function (mekan) {
        const ikon = L.divIcon({
            className: 'harita-isaret-kapsayici',
            html: '<span class="harita-isaret"><i class="bi ' + mekan.ikon + '"></i></span>',
            iconSize: [32, 32],
            iconAnchor: [16, 32],
            popupAnchor: [0, -30]
        });

        const isaret = L.marker([mekan.enlem, mekan.boylam], { icon: ikon, title: mekan.ad })
            .bindPopup(popupOlustur(mekan))
            .addTo(harita);

        isaretler.set(mekan.id, { isaret: isaret, tur: mekan.tur });
    });

    if (mekanlar.length > 0) {
        const sinirlar = L.latLngBounds(mekanlar.map(function (m) { return [m.enlem, m.boylam]; }));
        harita.fitBounds(sinirlar, { padding: [40, 40], maxZoom: 17 });
    }

    document.querySelectorAll('.filtre-dugme').forEach(function (dugme) {
        dugme.addEventListener('click', function () {
            const secilenTur = dugme.dataset.tur;

            document.querySelectorAll('.filtre-dugme').forEach(function (d) {
                d.classList.toggle('aktif', d === dugme);
            });

            isaretler.forEach(function (kayit) {
                const gorunsun = !secilenTur || kayit.tur === secilenTur;
                if (gorunsun) {
                    kayit.isaret.addTo(harita);
                } else {
                    kayit.isaret.remove();
                }
            });

            document.querySelectorAll('.mekan-listesi li').forEach(function (satir) {
                satir.hidden = !!secilenTur && satir.dataset.tur !== secilenTur;
            });
        });
    });

    document.querySelectorAll('.mekan-ogesi').forEach(function (oge) {
        oge.addEventListener('click', function () {
            const kayit = isaretler.get(Number(oge.dataset.id));
            if (!kayit) {
                return;
            }

            harita.flyTo(kayit.isaret.getLatLng(), 18);
            kayit.isaret.openPopup();

            if (window.innerWidth < 992) {
                haritaKutusu.scrollIntoView({ behavior: 'smooth', block: 'center' });
            }
        });
    });
})();
