using System;
using System.Collections.Generic;
using System.Text;

namespace Turkce
{
    /// <summary>
    /// Türkçe'de kelime çekimlemelerini yapan yardımcı sınıf. v1.1
    /// </summary>
    /// <remarks>
    /// Hal Ekleri: Muhammed Cuma Tahiroğlu, http://www.tahiroglu.com 
    /// Zaman Kipleri ve Çekimlemeler: Mustafa Burak Kalkan, mburakkalkan@gmail.com
    /// </remarks>
    public class TurkceEkler
    {
        /// <summary>
        /// Türkçe'de isme uygulanabilecek hâller
        /// </summary>
        static Dictionary<IsminHali, List<IHarf>> _haller = new Dictionary<IsminHali, List<IHarf>>();
        /// <summary>
        /// Türkçe'de fiile uygulanabilecek zaman kipleri
        /// </summary>
        static Dictionary<FiilKipi, List<IHarf>> _kipler = new Dictionary<FiilKipi, List<IHarf>>();
        /// <summary>
        /// Türkçe'de kişiler
        /// </summary>
        static Dictionary<Kisiler, List<IHarf>> _kisiler = new Dictionary<Kisiler, List<IHarf>>();

        public enum IsminHali
        {
            /// <summary>
            /// (e): dative(eve)
            /// </summary>
            Yonelme,
            /// <summary>
            /// (i): accusative(evi)
            /// </summary>
            Gosterme,
            /// <summary>
            /// (de):locative(evde)
            /// </summary>
            Bulunma,
            /// <summary>
            /// (den): ablative(evden)
            /// </summary>
            Ayrilma,
            /// <summary>
            /// (in): genitive ya da posessive(evin)
            /// </summary>
            Tamlama
        }

        public enum FiilKipi
        {
            GenisZaman,
            SimdikiZaman,
            GecmisZaman,
            MisliGecmisZaman,
            GelecekZaman,
            Emir,
            Gereklilik,
            DilekSart,
            Istek,
            Yeterlilik,
            Tezlik,
            GenisZamanOlumsuz,
            SimdikiZamanOlumsuz,
            GecmisZamanOlumsuz,
            MisliGecmisZamanOlumsuz,
            GelecekZamanOlumsuz,
            EmirOlumsuz,
            GereklilikOlumsuz,
            DilekSartOlumsuz,
            IstekOlumsuz,
            YeterlilikOlumsuz,
            TezlikOlumsuz
        }

        public enum Kisiler
        {
            BirinciTekil,
            IkinciTekil,
            UcuncuTekil,
            BirinciCogul,
            IkinciCogul,
            UcuncuCogul,
        }

        private enum KisiEkleri
        {
            Varsayilan,
            GecmisZaman,
            Istek,
            Emir
        }

        static TurkceEkler()
        {
            _haller.Add(IsminHali.Yonelme, new List<IHarf>(new IHarf[] { new KaynastirmaHarfi(Alfabe.Y), new DuzGenisSesliHarf() }));
            _haller.Add(IsminHali.Gosterme, new List<IHarf>(new IHarf[] { new KaynastirmaHarfi(Alfabe.Y), new DarSesliHarf() }));
            _haller.Add(IsminHali.Bulunma, new List<IHarf>(new IHarf[] { new Benzesme(Alfabe.D), new DuzGenisSesliHarf() }));
            _haller.Add(IsminHali.Ayrilma, new List<IHarf>(new IHarf[] { new Benzesme(Alfabe.D), new DuzGenisSesliHarf(), new SabitHarf(Alfabe.N) }));
            _haller.Add(IsminHali.Tamlama, new List<IHarf>(new IHarf[] { new KaynastirmaHarfi(Alfabe.N), new DarSesliHarf(), new SabitHarf(Alfabe.N) }));

            _kipler.Add(FiilKipi.GenisZaman, new List<IHarf>(new IHarf[] { new DuzGenisSesliHarf(), new SabitHarf(Alfabe.R) }));
            _kipler.Add(FiilKipi.SimdikiZaman, new List<IHarf>(new IHarf[] { new DarSesliHarf(), new SabitHarf(Alfabe.Y), new SabitHarf(Alfabe.O), new SabitHarf(Alfabe.R) }));
            _kipler.Add(FiilKipi.GecmisZaman, new List<IHarf>(new IHarf[] { new KaynastirmaHarfi(Alfabe.Y), new Benzesme(Alfabe.D), new DarSesliHarf() }));
            _kipler.Add(FiilKipi.MisliGecmisZaman, new List<IHarf>(new IHarf[] { new KaynastirmaHarfi(Alfabe.Y), new SabitHarf(Alfabe.M), new DarSesliHarf(), new SabitHarf(Alfabe.Ş) }));
            _kipler.Add(FiilKipi.GelecekZaman, new List<IHarf>(new IHarf[] { new DuzGenisSesliHarf(), new SabitHarf(Alfabe.C), new DuzGenisSesliHarf(), new SabitHarf(Alfabe.K) }));
            _kipler.Add(FiilKipi.Emir, new List<IHarf>(new IHarf[] { }));
            _kipler.Add(FiilKipi.Gereklilik, new List<IHarf>(new IHarf[] { new SabitHarf(Alfabe.M), new DuzGenisSesliHarf(), new SabitHarf(Alfabe.L), new DarSesliHarf() }));
            _kipler.Add(FiilKipi.DilekSart, new List<IHarf>(new IHarf[] { new SabitHarf(Alfabe.S), new DuzGenisSesliHarf() }));
            _kipler.Add(FiilKipi.Istek, new List<IHarf>(new IHarf[] { new DuzGenisSesliHarf() }));
            _kipler.Add(FiilKipi.Yeterlilik, new List<IHarf>(new IHarf[] { new DuzGenisSesliHarf(), new SabitHarf(Alfabe.B), new SabitHarf(Alfabe.İ), new SabitHarf(Alfabe.L), new SabitHarf(Alfabe.İ), new SabitHarf(Alfabe.R) }));
            _kipler.Add(FiilKipi.Tezlik, new List<IHarf>(new IHarf[] { new DarSesliHarf() , new SabitHarf(Alfabe.V), new SabitHarf(Alfabe.E), new SabitHarf(Alfabe.R) }));

            _kipler.Add(FiilKipi.GenisZamanOlumsuz, new List<IHarf>(new IHarf[] { new SabitHarf(Alfabe.M), new DuzGenisSesliHarf(), new SabitHarf(Alfabe.Z) }));
            _kipler.Add(FiilKipi.SimdikiZamanOlumsuz, new List<IHarf>(new IHarf[] { new SabitHarf(Alfabe.M), new DarSesliHarf(), new SabitHarf(Alfabe.Y), new SabitHarf(Alfabe.O), new SabitHarf(Alfabe.R) }));
            _kipler.Add(FiilKipi.GecmisZamanOlumsuz, new List<IHarf>(new IHarf[] { new SabitHarf(Alfabe.M), new DuzGenisSesliHarf(), new SabitHarf(Alfabe.D), new DarSesliHarf() }));
            _kipler.Add(FiilKipi.MisliGecmisZamanOlumsuz, new List<IHarf>(new IHarf[] { new SabitHarf(Alfabe.M), new DuzGenisSesliHarf(), new SabitHarf(Alfabe.M), new DarSesliHarf(), new SabitHarf(Alfabe.Ş) }));
            _kipler.Add(FiilKipi.GelecekZamanOlumsuz, new List<IHarf>(new IHarf[] { new SabitHarf(Alfabe.M), new DuzGenisSesliHarf(), new SabitHarf(Alfabe.Y), new DuzGenisSesliHarf(), new SabitHarf(Alfabe.C), new DuzGenisSesliHarf(), new SabitHarf(Alfabe.K) }));
            _kipler.Add(FiilKipi.EmirOlumsuz, new List<IHarf>(new IHarf[] { new SabitHarf(Alfabe.M), new DuzGenisSesliHarf() }));
            _kipler.Add(FiilKipi.GereklilikOlumsuz, new List<IHarf>(new IHarf[] { new SabitHarf(Alfabe.M), new DuzGenisSesliHarf(), new SabitHarf(Alfabe.M), new DuzGenisSesliHarf(), new SabitHarf(Alfabe.L), new DarSesliHarf() }));
            _kipler.Add(FiilKipi.DilekSartOlumsuz, new List<IHarf>(new IHarf[] { new SabitHarf(Alfabe.M), new DuzGenisSesliHarf(), new SabitHarf(Alfabe.S), new DuzGenisSesliHarf() }));
            _kipler.Add(FiilKipi.IstekOlumsuz, new List<IHarf>(new IHarf[] { new SabitHarf(Alfabe.M), new DuzGenisSesliHarf(), new SabitHarf(Alfabe.Y), new DuzGenisSesliHarf() }));
            _kipler.Add(FiilKipi.YeterlilikOlumsuz, new List<IHarf>(new IHarf[] { new DuzGenisSesliHarf(), new SabitHarf(Alfabe.M), new DuzGenisSesliHarf(), new SabitHarf(Alfabe.Z) }));
            _kipler.Add(FiilKipi.TezlikOlumsuz, new List<IHarf>(new IHarf[] { new SabitHarf(Alfabe.M), new DuzGenisSesliHarf(), new SabitHarf(Alfabe.Y), new DarSesliHarf(), new SabitHarf(Alfabe.V), new SabitHarf(Alfabe.E), new SabitHarf(Alfabe.R) }));

            _kisiler.Add(Kisiler.BirinciTekil, new List<IHarf>(new IHarf[] { new UnluTuremesi(), new SabitHarf(Alfabe.M) }));
            _kisiler.Add(Kisiler.IkinciTekil, new List<IHarf>(new IHarf[] { new SabitHarf(Alfabe.S), new DarSesliHarf(), new SabitHarf(Alfabe.N) }));
            _kisiler.Add(Kisiler.UcuncuTekil, new List<IHarf>(new IHarf[] { }));
            _kisiler.Add(Kisiler.BirinciCogul, new List<IHarf>(new IHarf[] { new KaynastirmaHarfi(Alfabe.Y), new DarSesliHarf(), new SabitHarf(Alfabe.Z) }));
            _kisiler.Add(Kisiler.IkinciCogul, new List<IHarf>(new IHarf[] { new SabitHarf(Alfabe.S), new DarSesliHarf(), new SabitHarf(Alfabe.N), new DarSesliHarf(), new SabitHarf(Alfabe.Z) }));
            _kisiler.Add(Kisiler.UcuncuCogul, new List<IHarf>(new IHarf[] { new SabitHarf(Alfabe.L), new DuzGenisSesliHarf(), new SabitHarf(Alfabe.R) }));

        }

        /// <summary>Verilen Türkçe ismi istenen hale göre çekimler.</summary>
        /// <param name="govde">Türkçe isim</param>
        /// <param name="hal">İsmin hâli</param>
        /// <returns>Çekimlenmiş ifade.</returns>
        public static string Uygula(string govde, IsminHali hal)
        {
            GovdeBilgisi govdeBilgisi = new GovdeBilgisi(govde);
            var harfler = _haller[hal];
            StringBuilder sb = new StringBuilder(govde);
            StringBuilder ek = new StringBuilder();

            foreach (var harf in harfler)
            {
                var c = harf.Bas(govdeBilgisi);
                if (c != char.MinValue) ek.Append(c);
            }

            // Yumuşama kontrolü
            if (ek.ToString() != "")
                if (govdeBilgisi.SertSessizIleBitiyor && Alfabe.Sesliler.Contains(ek[0]))
                    switch (govdeBilgisi.SonHarf)
                    {
                        case Alfabe.P: sb[sb.Length - 1] = Alfabe.B; break;
                        case Alfabe.Ç: sb[sb.Length - 1] = Alfabe.C; break;
                        case Alfabe.T: sb[sb.Length - 1] = Alfabe.D; break;
                        case Alfabe.K: sb[sb.Length - 1] = Alfabe.Ğ; break;
                    }

            return sb.Append(ek).ToString();
        }

        /// <summary>Verilen Türkçe fiili istenen zamana ve kişiye göre çekimler.</summary>
        /// <param name="fiilKoku">Türkçe fiil</param>
        /// <param name="kip">Zaman kipi</param>
        /// <param name="kisi">Kişi</param>
        /// <returns>Çekimlenmiş ifade.</returns>
        public static string Uygula(string fiilKoku, FiilKipi kip, Kisiler kisi)
        {
            GovdeBilgisi govdeBilgisi = new GovdeBilgisi(fiilKoku);
            var harfler = _kipler[kip];
            StringBuilder sb = new StringBuilder(fiilKoku);
            StringBuilder ek = new StringBuilder();

            // Bazı kipler için kişi ekleri değişik çekimlenecek (geliyor-sun, geldi-n gibi)
            if (kip == FiilKipi.GecmisZaman || kip == FiilKipi.GecmisZamanOlumsuz || kip == FiilKipi.DilekSart || kip == FiilKipi.DilekSartOlumsuz)
                KisiEkiDegistir(KisiEkleri.GecmisZaman);
            else if (kip == FiilKipi.Istek || kip == FiilKipi.IstekOlumsuz)
                KisiEkiDegistir(KisiEkleri.Istek);
            else if (kip == FiilKipi.Emir || kip == FiilKipi.EmirOlumsuz)
                KisiEkiDegistir(KisiEkleri.Emir);
            else
                KisiEkiDegistir(KisiEkleri.Varsayilan);

            foreach (var harf in harfler)
            {
                var c = harf.Bas(govdeBilgisi);
                if (c != char.MinValue) ek.Append(c);
            }

            // Yumuşama kontrolü
            if (ek.ToString() != "")
                if (Alfabe.InceSesliler.Contains(govdeBilgisi.SonSesli) && govdeBilgisi.SonHarf == Alfabe.T && Alfabe.Sesliler.Contains(ek[0]))
                    sb[sb.Length - 1] = Alfabe.D;

            sb.Append(ek);

            // Olumsuzlardaki -mez ekini birinci tekil kişi için temizleme
            if ((kisi == Kisiler.BirinciTekil || kisi == Kisiler.BirinciCogul) && sb[sb.Length - 1] == Alfabe.Z)
                sb.Remove(sb.Length - 1, 1);

            sb = new StringBuilder(KisiyeGoreCekimle(sb.ToString(), kisi));

            return sb.ToString();
        }

        /// <summary>Verilen Türkçe fiili istenen birleşik zamana ve kişiye göre çekimler.</summary>
        /// <param name="fiilKoku">Türkçe fiil</param>
        /// <param name="kip1">1. zaman kipi</param>
        /// <param name="kip2">2. zaman kipi</param>
        /// <param name="kisi">Kişi</param>
        /// <returns>Çekimlenmiş ifade.</returns>
        public static string Uygula(string fiilKoku, FiilKipi kip1, FiilKipi kip2, Kisiler kisi)
        {
            StringBuilder sb = new StringBuilder(fiilKoku);
            // 1. zamana çekimleme
            sb = new StringBuilder(Uygula(sb.ToString(), kip1, Kisiler.UcuncuTekil));
            // 2. zamana çekimleme
            sb = new StringBuilder(Uygula(sb.ToString(), kip2, kisi));

            return sb.ToString();
        }

        private static string KisiyeGoreCekimle(string fiilGovdesi, Kisiler kisi)
        {
            GovdeBilgisi govdeBilgisi = new GovdeBilgisi(fiilGovdesi);

            var harfler = _kisiler[kisi];

            StringBuilder sb = new StringBuilder(fiilGovdesi);
            StringBuilder ek = new StringBuilder();

            foreach (var harf in harfler)
            {
                var c = harf.Bas(govdeBilgisi);
                if (c != char.MinValue) ek.Append(c);
            }

            // Yumuşama kontrolü
            if (ek.ToString() != "")
            {
                if (Alfabe.SertSessizler.Contains(govdeBilgisi.SonHarf) && Alfabe.Sesliler.Contains(ek[0]))
                {
                    switch (govdeBilgisi.SonHarf)
                    {
                        case Alfabe.P: sb[sb.Length - 1] = Alfabe.B; break;
                        case Alfabe.Ç: sb[sb.Length - 1] = Alfabe.C; break;
                        case Alfabe.T: sb[sb.Length - 1] = Alfabe.D; break;
                        case Alfabe.K: sb[sb.Length - 1] = Alfabe.Ğ; break;
                    }
                }
            }

            return sb.Append(ek).ToString();
        }

        // Geçmiş zaman çekimlemesi diğerlerinden farklı
        private static void KisiEkiDegistir(KisiEkleri ek)
        {
            switch (ek)
            {
                case KisiEkleri.Varsayilan:
                    _kisiler[Kisiler.BirinciTekil] = new List<IHarf>(new IHarf[] { new UnluTuremesi(), new SabitHarf(Alfabe.M) });
                    _kisiler[Kisiler.IkinciTekil] = new List<IHarf>(new IHarf[] { new SabitHarf(Alfabe.S), new DarSesliHarf(), new SabitHarf(Alfabe.N) });
                    _kisiler[Kisiler.UcuncuTekil] = new List<IHarf>(new IHarf[] { });
                    _kisiler[Kisiler.BirinciCogul] = new List<IHarf>(new IHarf[] { new KaynastirmaHarfi(Alfabe.Y), new DarSesliHarf(), new SabitHarf(Alfabe.Z) });
                    _kisiler[Kisiler.IkinciCogul] = new List<IHarf>(new IHarf[] { new SabitHarf(Alfabe.S), new DarSesliHarf(), new SabitHarf(Alfabe.N), new DarSesliHarf(), new SabitHarf(Alfabe.Z) });
                    _kisiler[Kisiler.UcuncuCogul] = new List<IHarf>(new IHarf[] { new SabitHarf(Alfabe.L), new DuzGenisSesliHarf(), new SabitHarf(Alfabe.R) });
                    break;

                case KisiEkleri.GecmisZaman:
                    _kisiler[Kisiler.BirinciTekil] = new List<IHarf>(new IHarf[] { new SabitHarf(Alfabe.M) });
                    _kisiler[Kisiler.IkinciTekil] = new List<IHarf>(new IHarf[] { new SabitHarf(Alfabe.N) });
                    _kisiler[Kisiler.UcuncuTekil] = new List<IHarf>(new IHarf[] { });
                    _kisiler[Kisiler.BirinciCogul] = new List<IHarf>(new IHarf[] { new SabitHarf(Alfabe.K) });
                    _kisiler[Kisiler.IkinciCogul] = new List<IHarf>(new IHarf[] { new SabitHarf(Alfabe.N), new DarSesliHarf(), new SabitHarf(Alfabe.Z) });
                    _kisiler[Kisiler.UcuncuCogul] = new List<IHarf>(new IHarf[] { new SabitHarf(Alfabe.L), new DuzGenisSesliHarf(), new SabitHarf(Alfabe.R) });
                    break;

                case KisiEkleri.Istek:
                    _kisiler[Kisiler.BirinciTekil] = new List<IHarf>(new IHarf[] { new SabitHarf(Alfabe.Y), new DarSesliHarf(), new SabitHarf(Alfabe.M) });
                    _kisiler[Kisiler.IkinciTekil] = new List<IHarf>(new IHarf[] { new SabitHarf(Alfabe.S), new DarSesliHarf(), new SabitHarf(Alfabe.N) });
                    _kisiler[Kisiler.UcuncuTekil] = new List<IHarf>(new IHarf[] { });
                    _kisiler[Kisiler.BirinciCogul] = new List<IHarf>(new IHarf[] { new KaynastirmaHarfi(Alfabe.L), new DarSesliHarf(), new SabitHarf(Alfabe.M) });
                    _kisiler[Kisiler.IkinciCogul] = new List<IHarf>(new IHarf[] { new SabitHarf(Alfabe.S), new DarSesliHarf(), new SabitHarf(Alfabe.N), new DarSesliHarf(), new SabitHarf(Alfabe.Z) });
                    _kisiler[Kisiler.UcuncuCogul] = new List<IHarf>(new IHarf[] { new SabitHarf(Alfabe.L), new DuzGenisSesliHarf(), new SabitHarf(Alfabe.R) });
                    break;
                case KisiEkleri.Emir:
                    _kisiler[Kisiler.BirinciTekil] = new List<IHarf>(new IHarf[] { });
                    _kisiler[Kisiler.IkinciTekil] = new List<IHarf>(new IHarf[] { });
                    _kisiler[Kisiler.UcuncuTekil] = new List<IHarf>(new IHarf[] { new SabitHarf(Alfabe.S), new DarSesliHarf(), new SabitHarf(Alfabe.N) });
                    _kisiler[Kisiler.BirinciCogul] = new List<IHarf>(new IHarf[] { });
                    _kisiler[Kisiler.IkinciCogul] = new List<IHarf>(new IHarf[] { new KaynastirmaHarfi(Alfabe.Y), new DarSesliHarf(), new SabitHarf(Alfabe.N), new DarSesliHarf(), new SabitHarf(Alfabe.Z) });
                    _kisiler[Kisiler.UcuncuCogul] = new List<IHarf>(new IHarf[] { new SabitHarf(Alfabe.S), new DarSesliHarf(), new SabitHarf(Alfabe.N), new SabitHarf(Alfabe.L), new DuzGenisSesliHarf(), new SabitHarf(Alfabe.R) });
                    break;


            }

        }

        #region Alfabe

        static class Alfabe
        {
            public static HashSet<char> Sesliler = new HashSet<char>(new[] { A, E, I, İ, O, Ö, U, Ü });
            public static HashSet<char> DuzSesliler = new HashSet<char>(new[] { A, E, I, İ });
            public static HashSet<char> YuvarlakSesliler = new HashSet<char>(new[] { O, Ö, U, Ü });
            public static HashSet<char> DarSesliler = new HashSet<char>(new[] { I, İ, U, Ü });
            public static HashSet<char> GenisSesliler = new HashSet<char>(new[] { A, E, O, Ö });
            public static HashSet<char> KalinSesliler = new HashSet<char>(new[] { A, I, O, U });
            public static HashSet<char> InceSesliler = new HashSet<char>(new[] { E, İ, Ö, Ü });

            public static HashSet<char> SertSessizler = new HashSet<char>(new[] { F, S, T, K, Ç, Ş, H, P });
            public static HashSet<char> YumusakSessizler = new HashSet<char>(new[] { B, C, D, Ğ });

            public const char A = 'a';
            public const char B = 'b';
            public const char C = 'c';
            public const char Ç = 'ç';
            public const char D = 'd';
            public const char E = 'e';
            public const char F = 'f';
            public const char G = 'g';
            public const char Ğ = 'ğ';
            public const char H = 'h';
            public const char I = 'ı';
            public const char İ = 'i';
            public const char J = 'j';
            public const char K = 'k';
            public const char L = 'l';
            public const char M = 'm';
            public const char N = 'n';
            public const char O = 'o';
            public const char Ö = 'ö';
            public const char P = 'p';
            public const char R = 'r';
            public const char S = 's';
            public const char Ş = 'ş';
            public const char T = 't';
            public const char U = 'u';
            public const char Ü = 'ü';
            public const char V = 'v';
            public const char Y = 'y';
            public const char Z = 'z';


        }

        #endregion

        #region Harf

        interface IHarf
        {
            char Bas(GovdeBilgisi govde);
        }

        class KaynastirmaHarfi : IHarf
        {
            char _k;

            public KaynastirmaHarfi(char k)
            {
                _k = k;
            }

            public char Bas(GovdeBilgisi govde)
            {
                if (govde.SessizIleBitiyor)
                {
                    return char.MinValue;
                }
                else
                {
                    return _k;
                }
            }
        }

        class DuzGenisSesliHarf : IHarf
        {
            public char Bas(GovdeBilgisi govde)
            {
                if (Alfabe.KalinSesliler.Contains(govde.SonSesli))
                    return Alfabe.A;
                else
                    return Alfabe.E;
            }
        }

        class DarSesliHarf : IHarf
        {
            public char Bas(GovdeBilgisi govde)
            {
                switch (govde.SonSesli)
                {
                    case Alfabe.E:
                        return Alfabe.İ;
                    case Alfabe.A:
                        return Alfabe.I;
                    case Alfabe.O:
                        return Alfabe.U;
                    case Alfabe.Ö:
                        return Alfabe.Ü;
                    default:
                        return govde.SonSesli;
                }
            }
        }

        class UnluTuremesi : IHarf
        {
            public char Bas(GovdeBilgisi govde)
            {
                if (govde.SessizIleBitiyor)
                {
                    switch (govde.SonSesli)
                    {
                        case Alfabe.E: return Alfabe.İ;
                        case Alfabe.A: return Alfabe.I;
                        case Alfabe.O: return Alfabe.U;
                        case Alfabe.Ö: return Alfabe.Ü;
                        default: return govde.SonSesli;
                    }
                }
                else
                    return char.MinValue;
            }
        }

        class SabitHarf : IHarf
        {
            public char EsasHarf { get; set; }

            public SabitHarf(char c)
            {
                EsasHarf = c;
            }

            public char Bas(GovdeBilgisi govde)
            {
                return EsasHarf;
            }
        }

        class Benzesme : IHarf
        {
            char _b;

            public Benzesme(char b)
            {
                _b = b;
            }

            public char Bas(GovdeBilgisi govde)
            {
                if (Alfabe.SertSessizler.Contains(govde.SonHarf))
                {
                    switch (_b)
                    {
                        case Alfabe.B: _b = Alfabe.P; break;
                        case Alfabe.C: _b = Alfabe.Ç; break;
                        case Alfabe.D: _b = Alfabe.T; break;
                        case Alfabe.G: _b = Alfabe.K; break;
                        case Alfabe.Ğ: _b = Alfabe.K; break;
                        default: break;
                    }
                }

                return _b;
            }
        }

        #endregion

        class GovdeBilgisi
        {
            public char SonSesli { get; set; }
            public char SonHarf { get; set; }
            public bool SessizIleBitiyor { get; set; }
            public bool SertSessizIleBitiyor { get; set; }

            public GovdeBilgisi(string kaynak)
            {
                SonSesli = SonSesliyiVer(kaynak);
                SonHarf = kaynak[kaynak.Length - 1];
                SessizIleBitiyor = !Alfabe.Sesliler.Contains(SonHarf);
                SertSessizIleBitiyor = Alfabe.SertSessizler.Contains(SonHarf);
            }

            public char SonSesliyiVer(string t)
            {
                // son sesliyi, son dört harfte ara!
                for (int i = t.Length - 1; i > t.Length - 5 && i >= 0; i--)
                {
                    char c = t[i];
                    if (Alfabe.Sesliler.Contains(c))
                    {
                        return c;
                    }
                }
                return char.MinValue;
            }
        }
    }
}
