/*
 * IIS Projekt - Nemocnice
 * Listopad 2020
 * Autoři: Radek Veverka (xvever13)
 *         Adam Sedmík (xsedmi04)
 * Generated and edited file
 */

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

/** TODOs:

- DONE - Error pøi duplicitním pojišovacím reqestu (zmìnit primární klíè).
- DONE - Omezit upload na obrázky
- DONE - Pohledy pro pacienty
- DONE, OTESTOVAT -  Mazaní userù + delete constraints.
- DONE - Èeské chybové hlášky.
- DONE - Pøidat text "žádné zdravotní záznamy" pokud records = 0
- DONE - Možnost ukonèit záznam (posunout stav).
- DONE - Možnost dokonèit vyšetøení (posunout stav).
- DONE - Pøidat pole "Description" pøi vytváøení tiketu.
- DONE - Nefunguje vytvareni zaznamu - spatna prace s paramentrem
- DONE - Requesty pojišováka.
- DONE - Øazení všech tabulek a záznamù
- DONE - Barevné zvýrazòování dle stavu. - Asi neni potreba u recordu jsou zasedly a nekde se proste nezobrazujou kdyz nejsou otevreny (ticketact, tikety u lekare)
- DONE - LocalRedirect vs ReturnToPage??
- DONE - Oznaèit povinná formuláøová pole hvìzdièkou
- DONE - Dodat validation script partials (mozna primo do layoutu?)
- DONE - Nastylovat obrázky v reportu (max. velikost)
- DONE - Impelmentovat Aktivni tikety z pohledu lekare
- DONE - MedicalAct Create - cesky error zpravy, a zobrazovat CZK vedle cisla
- DONE - Doplnit titulky k odkazùm
- DONE - MELO by byt Doplnit jmena zalozek
- DONE - ASI Doplnit zpìtné navigaèní odkazy, kde se to hodí
- DONE -Poøešit maximální velikosti stringù (db sloupce a formy), zvýšit pro popisy.
- DONE Zkontrolovat a doplnit všechna oprávnìní na všechny stránky.
- DONE - MELO Pøidat k souborùm hlavièky - projekt, datum, autori...
- DONE - Tickety od jineho lekare by mel lekar spravujici zaznam videt?
- Seedování dat do databáze pøi migraci.

Bugs Found:
- DONE - Z admina na edit user zmena id na co neexistuje - crash
- DONE - Admin pokud tvori zaznam tak se mi nabidne jeho jmeno i kdyz neni doktor
- DONE - Pacient,Pojistovak muze vytvaret/editovat tikety, zaznamy (z url)
- DONE - Chybejici Authorize (nedavat u admina)
- DONE - Admin,Pacient,Pojistovak,jiny lekar se dostane na stranku tikety urciteho lekaru ale nevidi jeho tikety
- DONE - Lekarska zprava jde bez nadpisu
- DONE - admin muze vytvorit uzivatele bez role ale asi nevadi
- DONE - kdyz se vymaze posledni admin - tak rip ale asi nevadi
- DONE - Datum narozeni requred neni cesky
- DONE - Pacient vidi tlacitko dokoncit u Recordu (Ale i tak ma AccesDenied)
- DONE - u hesla se nevypisuje ze je povine kdyz neni zadany, ale vypisuje se kdyz se neshoduji
    - DONE - Pacient,Pojistovak,Admin se dostane na stranku se seznamem ticketu, nema tam zadne ale
    - DONE - Stranka se zadosti o tikety sice muze obsahovat id/username ale stejne zobrazi tikety toho uzivatele
- DONE zmena url u pacienta na neexistujiciho - lze vytvorit record na pacienta, ktery neexstiuje a pacientid bude null
-   - DONE pak se na to de dostat pokud znas recordid z url na reportbrowser a pokud se udela tiket a ten doktor si pak zobrazi tikety - crash
- DONE - pri zmene emailu zustane uzivatel prihlasen jako stary mail a pak teda nic nevidi
- DONE - Tlačitko Zpět na záznam je i u doktora, který obsluhuje tiket, a nemá zpět přístup
- DONE - Zmizel pomatovat prihlaseni checkbox
- DONE - Lekar muze editovat zaznamy jineho lekare a vytvaret tikety toho lekare (z url)
- DONE - Lze dokoncit zaznam i kdyz jeste tikety nejsou dokonceny
- DONE - Tiket lze editovat i po dokonceni, i pridavat ukony
*/

namespace iis_project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
