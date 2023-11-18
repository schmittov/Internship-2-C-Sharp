using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq.Expressions;

Dictionary<int, Artikal> artikli = new Dictionary<int, Artikal>(); // inicijalizacija dictionarya namjenjenog za artikle Key je integer
Dictionary<int, Radnik> radnici = new Dictionary<int, Radnik>(); //inicijalizacija dictionarya namjenjenog za radnike Key je integer
Dictionary<int, Racun> racuni = new Dictionary<int, Racun>(); //inicijalizacija dictionarya namjenjenog za račune Key je integer


//ARTIKLI, RADNICI DODANI ZBOG LAKŠE PROVJERE KODA
//                         id  ime       istek roka                    cijena kolicina
artikli.Add(1, new Artikal(1, "Mlijeko", DateTime.Parse("2023-12-01"), 1.5f, 10,0));
artikli.Add(2, new Artikal(2, "Kruh", DateTime.Parse("2023-11-01"), 1.3f, 5,0));
artikli.Add(3, new Artikal(3, "Šunka", DateTime.Parse("2023-11-20"), 1.0f, 20,0));

//                        id ime       prezime     datum rodenja
radnici.Add(1, new Radnik(1, "Mario", "Mitov", DateTime.Parse("2004-11-20")));
radnici.Add(2, new Radnik(2, "Josip", "Josipić", DateTime.Parse("2003-10-20")));
radnici.Add(3, new Radnik(3, "Marko", "Markic", DateTime.Parse("2002-09-20")));
radnici.Add(4, new Radnik(4, "Ivan", "Markic", DateTime.Parse("1902-09-20")));

int odabir1, odabir2;

start:
Console.WriteLine("Pritisnite bilo koju tipku za nastaviti");
Console.ReadKey();
Console.Clear();

do
{
    glavniIzbornik(); //ispis glavnog izbornika (metoda)
    Console.Write("\nOdaberite opciju: ");
    if (int.TryParse(Console.ReadLine(), out odabir1) && odabir1 >= 0 && odabir1 <= 4) //unos integera dok se ne unese (u rasponu od [0,4]) 
        break; 
    else
        Console.Clear();
} while (true);

if (odabir1 == 0)
    Environment.Exit(0);//izlaz iz aplikacije

Console.Clear();

do
{
    odabirSekundarnogIzbornika(odabir1); //Ispis podIzbornikaArtikli,Radnici itd.
    Console.Write("\nOdaberite opciju: ");
    if (int.TryParse(Console.ReadLine(), out odabir2) && odabir2 >= 0 && odabir2 <= 4) //odabir npr. za artikle (unos,brisanje,ispis...)
        break;
    else
        Console.Clear();
} while (true);


if (odabirTercijalnogIzbornika(odabir1, odabir2) == 1) 
{
    radnjeArtikli(odabir2, artikli,racuni); //zvanje funkcije s artiklima 
}
else if (odabirTercijalnogIzbornika(odabir1, odabir2) == 2)
{
    radnjeRadnici(odabir2, radnici); //zvanje funkcije s radnicima
}
else if (odabirTercijalnogIzbornika(odabir1, odabir2) == 3) 
{
    radnjeRacuni(odabir2, artikli, racuni);
}
else if (odabirTercijalnogIzbornika(odabir1, odabir2) == 4)
{
    radnjeStatistika(odabir2,artikli,radnici,racuni);
}
    goto start;
//kraj "funkcionalnog" koda



//METODE ZA ISPIS (ne toliko bitne)
static void glavniIzbornik()
{
    Console.WriteLine("1-Artikli");
    Console.WriteLine("2-Radnici");
    Console.WriteLine("3-Racuni");
    Console.WriteLine("4-Statistika");
    Console.WriteLine("0-Izlaz iz aplikacije");
}
static void podIzbornikArtikli() //sekundarni izbornik
{
    Console.WriteLine("1-Unos artikla");
    Console.WriteLine("2-Brisanje artikla");
    Console.WriteLine("3-Uređivanje artikla");
    Console.WriteLine("4-Ispis");
}
static void podIzbornikRadnici() //sekundarni izbornik
{
    Console.WriteLine("1-Unos radnika");
    Console.WriteLine("2-Brisanje radnika");
    Console.WriteLine("3-Uređivanje radnika");
    Console.WriteLine("4-Ispis");
}
static void podIzbornikRacuni() //sekundarni izbornik
{
    Console.WriteLine("1-Unos novog računa");
    Console.WriteLine("2-Ispis");
}
static void podIzbornikStatistika() //sekundarni izbornik
{
    Console.Clear();
    Console.WriteLine("1-Ukupan broj artikala u trgovini");
    Console.WriteLine("2-Vrijednost artikala koji nisu još prodani");
    Console.WriteLine("3-Vrijednost svih artikala koji su prodani (nalaze se na računima)");
    Console.WriteLine("4-Stanje po mjesecima");
}

//Metode za ispis s uvjetima (ne toliko bitno)
static void odabirSekundarnogIzbornika(int x)
{
    if (x == 1) podIzbornikArtikli();
    else if (x == 2) podIzbornikRadnici();
    else if (x == 3) podIzbornikRacuni();
    else if (x == 4)
    {
        int i = 0;
        Console.WriteLine("Unesite lozinku za pristup");
        do
        {
            if ((Console.ReadLine()) == GlobalneVarijable.Lozinka)
                podIzbornikStatistika();
            else
            {
                Console.WriteLine("Unijeli ste netočnu lozinku.");
                i++;
                if(i==3)
                    Environment.Exit(0);//izlaz iz aplikacije ukoliko se pogriješi lozinka više od 3 puta
            }
        } while (true);
    }
    else if (x == 0) ;

    else Console.WriteLine("Unijeli ste nepostojeću opciju");
    
}
static int odabirTercijalnogIzbornika(int x,int y)
{
    if (x == 1)         return 1;
    else if (x == 2)    return 2;
    else if (x == 3)    return 3;
    else if (x == 4)    return 4;
    else                return 0;
    
}
static void radnjeArtikli(int x, Dictionary<int, Artikal> artikli, Dictionary<int, Racun> racuni) //metoda koja bira koju radnju obaviti s artiklom, odnosno koju metodu pozvati
{
    char y;
    Console.Clear();    
    if (x == 1)
    {
        unosArtikala(artikli);
    }
    else if (x == 2)
    {
        Console.WriteLine("Brisanje artikala\n a-Po imenu artikla\n b-Kojima je istekao rok");
        do
        {
            string unos = Console.ReadLine();
            if (unos.Length == 1)
            {
                if (char.TryParse(unos, out y) && (y == 'a' || y == 'b'))
                {
                    break;
                }
            }
            Console.WriteLine("Unijeli ste nepostojeću opciju.");
        } while (true);
        brisanjeArtikala(y, artikli);
    }
    else if (x == 3)
    {
        
        Console.WriteLine("Uređivanje artikla\n a-Zasebno proizvoda\n b-Popopust/poskupljenje");
        do
        {
            string unos = Console.ReadLine();
            if (unos.Length == 1)
            {
                if (char.TryParse(unos, out y) && (y == 'a' || y == 'b'))
                {
                    break;
                }
            }
            Console.WriteLine("Unijeli ste nepostojeću opciju.");
        } while (true);
        uredivanjeArtikala(y, artikli);
    }
    else if (x == 4)
    {
        
        Console.WriteLine("Ispiši artikle\n a-Kako su spremljeni\n b-Svih sortiranih po imenu\n c-Uzlazno sortirano po datumu\n d-Silazno sortirani po datumu \n e-Sortiranih po količini\n f-Najprodavaniji artikal\n g-Najmanjeprodavaniji artikal");
        do
        {
            string unos = Console.ReadLine();
            if (unos.Length == 1)
            {
                if (char.TryParse(unos, out y) && (y == 'a' || y == 'b' || y=='c' || y=='d' || y=='e' || y == 'f' || y == 'g'))
                {
                    break;
                }
            }
            Console.WriteLine("Unijeli ste nepostojeću opciju.");
        } while (true);
        ispisArtikala(y, artikli, racuni);
    }
}
static void radnjeRadnici(int x,Dictionary<int, Radnik> radnici) //metoda koja bira koju radnju obaviti s radnikom, odnosno koju metodu pozvati
{
    char y;
    if (x == 1)
    {
        unosRadnika(radnici);
    }
    else if (x == 2)
    {
        
        Console.Clear();
        Console.WriteLine("Brisanje radnika\n a-Po imenu artikla\n b-Starijih od 65");
        do
        {
            string unos = Console.ReadLine();
            if (unos.Length == 1)
            {
                if (char.TryParse(unos, out y) && (y == 'a' || y == 'b'))
                {
                    break;
                }
            }
            Console.WriteLine("Unijeli ste nepostojeću opciju.");
        } while (true);
        brisanjeRadnika(y, radnici);
    }
    else if (x == 3)
    {
        uredivanjeRadnika(radnici);
    }
    else if (x == 4)
    {
        Console.Clear();
        Console.WriteLine("Ispis radnika\n a-svih radnika\n b-Kojima je rođendan u tekućem mjesecu");
        do
        {
            string unos = Console.ReadLine();
            if (unos.Length == 1)
            {
                if (char.TryParse(unos, out y) && (y == 'a' || y == 'b'))
                {
                    break;
                }
            }
            Console.WriteLine("Unijeli ste nepostojeću opciju.");
        } while (true);
        ispisRadnika(y, radnici);
    }
}
static void radnjeRacuni(int x, Dictionary<int, Artikal> artikli, Dictionary<int, Racun> racuni)
{

    if(x == 1) 
    {
        ispisArtikala('a',artikli,racuni);
        float iznos=0;


        int idRacuna = ++GlobalneVarijable.BrojacIdRacuna;

        Dictionary<string, int> proizvodiNaRacunu = new Dictionary<string, int>();
        do
        {
            Console.Write("Unesi ime proizvoda: ");
            string ime=Console.ReadLine();
            Console.Write("Unesi količinu proizvoda: ");
            int kolicina=int.Parse(Console.ReadLine());
            proizvodiNaRacunu.Add(ime, kolicina);
            foreach (var kvp in artikli)
            {
                if (kvp.Value.imeArtikla == ime)
                {
                    kvp.Value.kolicinaArtikla -= kolicina;
                    kvp.Value.prodanostArtikla += kolicina;
                    iznos += kvp.Value.cijenaArtikla*kolicina; 
                }
                    
            }
            Console.WriteLine("Ukoliko ste gotovi napišite 'Da', ako niste napišite bilo šta drugo");
        }
        while (Console.ReadLine()!="Da");
        
        DateTime datumIzdavanja = DateTime.Now;
        
        racuni.Add(idRacuna, new Racun(idRacuna, datumIzdavanja,iznos, proizvodiNaRacunu));
        Console.Write("Upisan je racun: ");

        Console.Write("ID: "+idRacuna);
        Console.Write(" Iznos: "+iznos);
        Console.Write(" Vrijeme izdavanja računa: "+datumIzdavanja+"\n");

    }
    else if(x == 2)
    {
        foreach(var kvp in racuni)
        {
            Console.Write($"ID:{kvp.Value.IdRacuna} ");
            Console.Write($"Datum izdavanja računa:{kvp.Value.DatumIzdavanja} ");
            Console.Write($"Iznos računa:{kvp.Value.Iznos} \n");
        }
        Console.WriteLine("Upišite 'i' ukoliko želite detalje o jednom računu, ako ne upišite bilo što drugo");
        if (Console.ReadLine() == "i")
        {
            Console.Write("Odaberite ID računa:");
            int a=int.Parse(Console.ReadLine());
            foreach(var kvp in racuni)
            {
                if (kvp.Value.IdRacuna == a)
                {
                    Console.Write($"ID:{kvp.Value.IdRacuna} ");
                    Console.Write($"Datum izdavanja računa:{kvp.Value.DatumIzdavanja} ");
                    Console.Write($"Iznos računa:{kvp.Value.Iznos} \n");

                    foreach (var proizvod in kvp.Value.Proizvodi)
                    {
                        Console.WriteLine($"{proizvod.Key}: {proizvod.Value}");
                    }
                }
            }
        }
    }
}
static void radnjeStatistika(int x, Dictionary<int, Artikal> artikli, Dictionary<int, Radnik> radnici, Dictionary<int, Racun> racuni)
{
    if(x == 1)
    {
        Console.WriteLine("U trgovini je ostalo još " + ukupanBrojArtikalaStatistika(artikli)+" artikala.");
    }
    else if(x == 2) 
    {
        Console.WriteLine("Vrijednost neprodanih artikala je " + vrijednostNeprodanihArtikalaStatistika(artikli)+" eura");

    }
    else if( x == 3) 
    {
        Console.WriteLine("Vrijednost prodanih artikala je " + vrijednostProdanihArtikalaStatistika(racuni) + " eura");
    }
    else if (x == 4)
    {
        Console.WriteLine("Zaradili ste "+stanjePoMjesecimaStatistika(racuni,radnici)+" eura");
    }
}


//metoda za artikale
static void unosArtikala(Dictionary<int, Artikal> artikli) //metoda kojom unosimo nove artikle
{
    Console.Clear();
    Console.WriteLine("Unos artikla");
    
    int idArtikla= GlobalneVarijable.BrojacIdArtikla++;
    Console.Write("ID: "+idArtikla+"\n");
    Console.Write("Ime: ");
    string imeArtikla=Console.ReadLine();
    Console.Write("Cijena: ");
    float cijenaArtikla=int.Parse(Console.ReadLine());
    Console.Write("Kolicina: ");
    int kolicinaArtikla=int.Parse(Console.ReadLine());

    Artikal noviArtikal = new Artikal(idArtikla, imeArtikla, DateTime.Now, cijenaArtikla,kolicinaArtikla,0);
    artikli.Add(idArtikla, noviArtikal);
    Console.WriteLine("Podaci o artiklu su uspješno uneseni.");
}
static void brisanjeArtikala(char x,Dictionary<int,Artikal> artikli) //metoda kojom brišemo postojeće artikle
{
    if(x == 'a')
    {
        Console.WriteLine("Unesite ime artikla kojeg želite obrisati");
        string imeZaBrisanje=Console.ReadLine();
        foreach(var kvp in artikli) //prođi sve artikle
        {
            if (kvp.Value.imeArtikla == imeZaBrisanje)//ako je uneseno ime jednako imenu trenutnog artikla
            {
                artikli.Remove(kvp.Key); //izbrisi taj artikal preko njegovog ID-ja
            }
        }
    }
    else if (x == 'b')
    {
        foreach(var kvp in artikli)
        {
            if (kvp.Value.datumIstekaRokaArtikla<=DateTime.Now)
            {
                artikli.Remove(kvp.Key);
            }
        }
    }
}
static void uredivanjeArtikala(char x, Dictionary<int, Artikal> artikli)
{
    char y;
    if (x== 'a') 
    {
        Console.WriteLine("Unesite ime artikla kojeg želite urediti");
        string imeZaUredivanje = Console.ReadLine();
        foreach (var kvp in artikli) //prođi sve artikle
        {
            
            if (kvp.Value.imeArtikla == imeZaUredivanje)//ako je uneseno ime jednako imenu trenutnog artikla
            {
                
                Console.WriteLine("Uredite\n a-Ime artikla\n b-Cijenu artikla\n c-Datum isteka roka"); //ispis
                do
                {
                    string unos = Console.ReadLine();
                    if (unos.Length == 1)
                    {
                        if (char.TryParse(unos, out y) && (y == 'a' || y == 'b' || y=='c'))
                        {
                            break;
                        }
                    }
                    Console.WriteLine("Unijeli ste nepostojeću opciju.");
                } while (true);
                if (y == 'a')
                {
                    artikli[kvp.Key].imeArtikla=Console.ReadLine(); //unos novog imena preko starog
                }
                else if (y == 'b')
                {
                    artikli[kvp.Key].cijenaArtikla = int.Parse(Console.ReadLine()); //unos nove cijene preko stare
                }
                else if(y == 'c')
                {
                    artikli[kvp.Key].datumIstekaRokaArtikla = DateTime.Parse(Console.ReadLine()); //unos novog datuma preko starog
                } 
            }
        }
    }
    else if(x== 'b') 
    {
        Console.WriteLine("Unesite postotak poskupljenja/sniženja");
        float promjenaCijene;
        do
        {
            string input = Console.ReadLine();
            if (float.TryParse(input, out promjenaCijene) && promjenaCijene > 0)
            {
                break;
            }
        } while (true);
        foreach (var kvp in artikli) //prođi sve artikle
        {
            artikli[kvp.Key].cijenaArtikla*= promjenaCijene/100; //pomnoži sve cijene artikla sa popustom/sniženjem
        }
    }
}
static void ispisArtikala(char x,Dictionary<int, Artikal> artikli, Dictionary<int, Racun> racuni) //metoda kojom ispisujemo artikle
{
    //
    //Console.WriteLine("Ispiši artikle\n a-Kako su spremljeni\n b-Svih sortiranih po imenu\n c-Uzlazno sortirano po datumu\n d-Silazno sortirani po datumu \n e-Sortiranih po količini\n f-Najprodavaniji artikal\n g-Najmanjeprodavaniji artikal");
    Console.Clear();


        if (x == 'a')
        {
            foreach (var kvp in artikli)
            {
                Console.Write($"ID:{kvp.Value.idArtikla} ");
                Console.Write($"Ime:{kvp.Value.imeArtikla} ");
                Console.Write($"Datum isteka roka:{kvp.Value.datumIstekaRokaArtikla} ");
                Console.Write($"Cijena:{kvp.Value.cijenaArtikla} ");
                Console.Write($"Količina:{kvp.Value.kolicinaArtikla} \n");
            }
        }
    
        else if (x == 'b')
        {
            Console.WriteLine("Sortirani po imenu:");
            var sortiraniArtikli = artikli.OrderBy(kv => kv.Value.imeArtikla);   
            foreach (var kvp in sortiraniArtikli)
            {
                Console.Write($"ID:{kvp.Value.idArtikla} ");
                Console.Write($"Ime:{kvp.Value.imeArtikla} ");
                Console.Write($"Datum isteka roka:{kvp.Value.datumIstekaRokaArtikla} ");
                Console.Write($"Cijena:{kvp.Value.cijenaArtikla} ");
                Console.Write($"Količina:{kvp.Value.kolicinaArtikla} \n");
            }

        }
        else if (x == 'c')
        {
            Console.WriteLine("Uzlazno sortirano po datumu:");
            var sortiraniArtikli = artikli.OrderBy(kv => kv.Value.datumIstekaRokaArtikla);
            foreach (var kvp in sortiraniArtikli)
            {
                Console.Write($"ID:{kvp.Value.idArtikla} ");
                Console.Write($"Ime:{kvp.Value.imeArtikla} ");
                Console.Write($"Datum isteka roka:{kvp.Value.datumIstekaRokaArtikla} ");
                Console.Write($"Cijena:{kvp.Value.cijenaArtikla} ");
                Console.Write($"Količina:{kvp.Value.kolicinaArtikla} \n");
            }
        }
        else if (x == 'd')
        {
            Console.WriteLine("Silazno sortirani po datumu:");
            var sortiraniArtikli = artikli.OrderByDescending(kv => kv.Value.datumIstekaRokaArtikla);   
            foreach (var kvp in sortiraniArtikli)
            {
                Console.Write($"ID:{kvp.Value.idArtikla} ");
                Console.Write($"Ime:{kvp.Value.imeArtikla} ");
                Console.Write($"Datum isteka roka:{kvp.Value.datumIstekaRokaArtikla} ");
                Console.Write($"Cijena:{kvp.Value.cijenaArtikla} ");
                Console.Write($"Količina:{kvp.Value.kolicinaArtikla} \n");
            }
        }
        else if (x == 'e')
        {
            Console.WriteLine("Sortiranih po količini:");
            var sortiraniArtikli = artikli.OrderByDescending(kv => kv.Value.kolicinaArtikla);
            foreach (var kvp in sortiraniArtikli)
            {
                Console.Write($"ID:{kvp.Value.idArtikla} ");
                Console.Write($"Ime:{kvp.Value.imeArtikla} ");
                Console.Write($"Datum isteka roka:{kvp.Value.datumIstekaRokaArtikla} ");
                Console.Write($"Cijena:{kvp.Value.cijenaArtikla} ");
                Console.Write($"Količina:{kvp.Value.kolicinaArtikla} \n");;
            }
        }
        else if (x == 'f')
        {
            
            Console.Write("Najprodavaniji artikal: ");
            Console.WriteLine(artikli.OrderByDescending(pair => pair.Value.prodanostArtikla).First().Value.imeArtikla);
        

        }
        else if (x == 'g')
        {
            Console.WriteLine("Najmanjeprodavaniji artikal:");
            Console.WriteLine(artikli.OrderByDescending(pair => pair.Value.prodanostArtikla).Last().Value.imeArtikla);
        }


}

//metoda za radnike
static void unosRadnika(Dictionary<int, Radnik> radnici) //metoda kojom unosimo radnike
{
    int idRadnika = GlobalneVarijable.BrojacIdRadnika++;
    Console.Write("ID: "+idRadnika + "\n");
    Console.Write("Ime: ");
    string imeRadnika = Console.ReadLine();
    Console.Write("Prezime: ");
    string prezimeRadnika = Console.ReadLine();

    Radnik noviRadnik = new Radnik(idRadnika, imeRadnika, prezimeRadnika, DateTime.Now);
    radnici.Add(idRadnika, noviRadnik);
    Console.WriteLine("Podaci o radniku su uspješno uneseni.");
} //metoda za unos radnika
static void uredivanjeRadnika(Dictionary<int, Radnik> radnici)
{
    Console.WriteLine("Unesite ime radnika kojeg želite urediti");
    string imeZaUredivanje = Console.ReadLine();
    Console.WriteLine("Unesite prezime radnika kojeg želite urediti");
    string prezimeZaUredivanje = Console.ReadLine();
    foreach (var kvp in radnici) //prođi sve artikle
    {
        if (kvp.Value.imeRadnika == imeZaUredivanje && kvp.Value.prezimeRadnika == prezimeZaUredivanje)//ako je uneseno ime jednako imenu trenutnog artikla
        {
            Console.WriteLine("Unesi nove podatke za radnika");
            Console.Write("Ime: ");
            radnici[kvp.Key].imeRadnika = Console.ReadLine(); //unos novog imena preko starog
            Console.Write("Prezime: ");
            radnici[kvp.Key].prezimeRadnika = Console.ReadLine(); //unos novog imena preko starog
            Console.Write("Datum rođenja: ");
            radnici[kvp.Key].datumRodenjaRadnika= DateTime.Parse(Console.ReadLine());
        }
    }
}//metoda za uređivanje radnika
static void brisanjeRadnika(char x,Dictionary<int, Radnik> radnici) //metoda kojom brišemo radnike //dodaj biranje a,b 
{
    if (x == 'a')
    {
        Console.WriteLine("Unesite ime radnika kojeg želite obrisati");
        string imeZaBrisanje = Console.ReadLine();
        foreach (var kvp in radnici) //prođi sve artikle
        {
            if (kvp.Value.imeRadnika == imeZaBrisanje)//ako je uneseno ime jednako imenu trenutnog artikla
            {
                radnici.Remove(kvp.Key); //izbrisi taj artikal preko njegovog ID-ja
            }
        }
    }
    else if (x == 'b')
    {
        foreach (var kvp in radnici)
        {
            if (kvp.Value.datumRodenjaRadnika <= DateTime.Now.AddYears(-65))
            {
                radnici.Remove(kvp.Key);
            }
        }
    }
    
}//metoda za brisanje radnika
static void ispisRadnika(char x,Dictionary<int, Radnik> radnici) //metoda kojom ispisujemo radnike
{

    if (x == 'a') Console.WriteLine("Kako su spremljeni:");

    else if (x == 'b') Console.WriteLine("Radnici s rođendanom u tekućem mjesecu:");
     
    foreach (var radnik in radnici.Values)
    {
        if (x == 'a')
        {
            Console.Write($"ID:{radnik.idRadnika} ");
            Console.Write($"Ime:{radnik.imeRadnika} ");
            Console.Write($"Prezime:{radnik.prezimeRadnika} ");
            Console.Write($"Datum rođenja:{radnik.datumRodenjaRadnika} \n");
        }
        else if(x == 'b')
        {
            if (radnik.datumRodenjaRadnika.Month == DateTime.Now.Month)
            {
                Console.Write($"ID:{radnik.idRadnika} ");
                Console.Write($"Ime:{radnik.imeRadnika} ");
                Console.Write($"Prezime:{radnik.prezimeRadnika} ");
                Console.Write($"Datum rođenja:{radnik.datumRodenjaRadnika} \n");
                
            }
        }
    }
}

//metoda za statistiku
static int ukupanBrojArtikalaStatistika(Dictionary<int, Artikal> artikli)
{
    int ukupanBrojArtikala = 0;
    foreach (var kvp in artikli)
    {
        ukupanBrojArtikala += kvp.Value.kolicinaArtikla;
    }
    return ukupanBrojArtikala;
}
static float vrijednostNeprodanihArtikalaStatistika(Dictionary<int, Artikal> artikli)
{
    float vrijednostNeprodanihArtikala = 0;
    foreach (var kvp in artikli)
    {
        vrijednostNeprodanihArtikala += kvp.Value.cijenaArtikla* kvp.Value.kolicinaArtikla;
    }
    return vrijednostNeprodanihArtikala;
}
static float vrijednostProdanihArtikalaStatistika(Dictionary<int, Racun> racuni)
{
    float vrijednostProdanihArtikala = 0;
    foreach (var kvp in racuni)
    {
        vrijednostProdanihArtikala += kvp.Value.Iznos;
    }
    return vrijednostProdanihArtikala;
}
static float stanjePoMjesecimaStatistika(Dictionary<int, Racun> racuni, Dictionary<int, Radnik> radnici)
{
    Console.Write("Unesite godinu: ");
    int godina = int.Parse(Console.ReadLine());
    Console.Write("Unesite mjesec: ");
    int mjesec = int.Parse(Console.ReadLine());

    DateTime unosMjesecaGodine = new DateTime(godina, mjesec, 1);

    Console.Write("Unesite iznos plaća radnicima: ");
    float placa=float.Parse(Console.ReadLine());

    int i = 0;
    foreach (var kvp in radnici)
    {
        i++;
    }
    placa = placa * i;

    Console.Write("Unesite iznos najma: ");
    float najam = float.Parse(Console.ReadLine());

    float zaradaRacuni = 0;
    foreach(var kvp in racuni)
    {
        DateTime datumRacuna = kvp.Value.DatumIzdavanja;
        DateTime zaokruzenDatum = new DateTime(datumRacuna.Year, datumRacuna.Month, 1);
        if (zaokruzenDatum == unosMjesecaGodine)
        {
            zaradaRacuni += kvp.Value.Iznos;
        }
    }
    return (zaradaRacuni / 3 - placa - najam);
}

//klase s proizvodima, radnicima ....
class Artikal
{
    public int idArtikla { get; }
    public string imeArtikla { get; set; }
    public DateTime datumIstekaRokaArtikla { get; set; }
    public float cijenaArtikla { get; set; }
    public int kolicinaArtikla { get; set; }
    public int prodanostArtikla { get; set; }

    public Artikal(int idArtikla, string imeArtikla, DateTime datumIstekaRokaArtikla, float cijenaArtikla, int kolicinaArtikla, int prodanostArtikla)
    {
        this.idArtikla = idArtikla;
        this.imeArtikla = imeArtikla;
        this.datumIstekaRokaArtikla = datumIstekaRokaArtikla;
        this.cijenaArtikla = cijenaArtikla;
        this.kolicinaArtikla = kolicinaArtikla;
        this.prodanostArtikla = prodanostArtikla;
    }
}
class Radnik
{
    public int idRadnika { get; }
    public string imeRadnika { get; set; }
    public string prezimeRadnika { get; set; }
    public DateTime datumRodenjaRadnika { get; set; }

    public Radnik(int idRadnika, string imeRadnika, string prezimeRadnika, DateTime datumRodenjaRadnika)
    {
        this.idRadnika = idRadnika;
        this.imeRadnika = imeRadnika;
        this.prezimeRadnika = prezimeRadnika;
        this.datumRodenjaRadnika = datumRodenjaRadnika;
    }
}
class Racun
{
    public int IdRacuna { get; }
    public float Iznos {  get; }
    public DateTime DatumIzdavanja { get; }
    public Dictionary<string, int> Proizvodi { get; }

    public Racun(int idRacuna, DateTime datumIzdavanja,float iznos, Dictionary<string, int> proizvodi)
    {
        this.IdRacuna = idRacuna;
        this.DatumIzdavanja = datumIzdavanja;
        this.Proizvodi = proizvodi;
        this.Iznos = iznos;
    }
}
public static class GlobalneVarijable
{
    public static int BrojacIdRacuna { get; set; } = 0;
    public static int BrojacIdArtikla { get; set; } = 4;//stavljeno je 4 jer smo na početku koda unijeli 3 artikla
    public static int BrojacIdRadnika { get; set; } = 4;
    public static string Lozinka = "meow";
}