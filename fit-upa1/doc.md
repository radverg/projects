# 1. projekt UPA - NoSQL - tým xsedmi04 a xvever13


## Analýza zdrojových dat a návrh jejich uložení v NoSQL databázi

Předmětem projektu bylo zpracovat veřejně dostupná data týkající se jízdních řádku vlakové dopravy, tato data následně zpracovat a 
uložit do databáze navržené tak, aby bylo možné provádět rychlé vyhledávání spojů při zadaném dni a času a počáteční a cílové stanice.
V první fáze projektu bylo třeba prozkoumat data, jejich strukturu a způsob jejich distribuce.

Informace o spojích jsou předávány do centrálního informačního systém pomocí XML zpráv. Každá XML zpráva je uložena v samostatém
XML souboru a obsahuje tedy jeden kořenový element `CZPTTCISMessage`. Tyto XML soubory jsou dále zabaleny do archivu ZIP a uloženy
na veřejně dostupném serveru `https://portal.cisjr.cz/pub/draha/celostatni/szdc/`, rozdělené do složek podle let. Každý rok 
obsahuje jeden velký balík výchozích souborů a mnoho průběžně dodávaných XML zpráv modifikujících výchozí jízdní řád. Tyto dodatečné soubory 
jsou děleně do složek dle měsíců.

Mezi běžnými zprávami se vyskytují i speciální zprávy rušící naplánovaný jízdní řád pro specifické dny. 

V důsledku nedostatku času jsme zvládli v rámci tohoto projektu provést implementaci pouze v nejzákladnější podobě,
kdy se stahuje a zpracovává pouze zmiňovaný velký archiv výchozích dat. 

XML zpráva definující řád spoje obsahuje následující relevantní informace k projektu, které jsou různě zanořené ve struktuře zprávy:
- Identifikace zprávy
- Posloupnost stanic spoje a časy v nich
- Kalendář spoje, který definuje pomocí bitové masky, ve kterých dnech v oboru platnosti spoje spoj jede

XML zpráva rovněž obsahuje spoustu položek pro projekt irelevantních a položky zatím neznáme a nedefinované, které mohou být 
do zprávy poskytovatelem vkládány libovolně. Dle zadání je však třeba všechny informace uchovat.
Toto by bylo problematické při užití SQL databáze, nicméně síla NoSQL databází spočívá mimo jiné v možnosti ukládat dynamicky libovolně
strukturovaná data. 

## Návrh způsobu uložení dat

Databáze musí být navržena především tak, aby bylo umožněno rychlé vyhledávání spojů. Zároveň musí být uložena všechna 
data dostupná ve zdrojových XML souborech. 

Po zvážení možností jsme se rozhodli ukládat zprávy přesně tak, jak jsou poskytovány a pro účely rychlého vyhledávání
udržovat pomocnou kolekci na tyto zprávy se odkazující. Jelikož vyhledáváme dle lokací a času, bude zřejmě užitečná podpůrná kolekce 
s lokacemi jako klíči pro okamžitý přístup s konstatní časovou složitostí. Právě tak jsme navrhli pomocnou kolekci - každá lokace
má pak seznam referencí na všechny původní zprávy, které tuto lokaci zahrnují. Další možností bylo zřídit pomocnou 
kolekci s časovými klíči, protože podle data času se vyhledává také. 

Přidání další zprávy je pak jednoduché a spočívá v načtení zprávy a jejím rozparsováním do datových struktur programovacího jazyka, určit 
klíč záznamu a záznam vložení do primární do kolekce zpráv. Následuje extrakce všech stanic ze zprávy a přidání reference na tuto
zprávu do všech klíču (názvů stanic) pomocné kolekce. 

Vyhledání spoje by pak mělo být velmi rychlé - prvně se naleznou v pomocné kolekci seznamy referencí na zprávy pro cílovou i počáteční 
stanici, což je díky zvoleným klíčům provedeno okamžitě, tedy s konstatní složitostí. Následně se provede v aplikačním kódu 
průnik těchto referencí a získají se tak pouze spoje, které obsahují obě stanice. Těchto referencí je už poměrně málo a může proběhnout
jejich dohledání a další zpracování v hlavní kolekci okamžitě opět na základě klíče. 
 
## Zvolená NoSQL databáze 

Pro implementaci jsme zvolili dokumentovou databází `MongoDB`. Při zpětné úvaze jsme však došli k tomu, že vzhledem k povaze návrhu, 
který prakticky nezahrnuje žádné složité dotazy a vyhledává a vkládá jen na základě klíčů,
dávalo větší smysl zvolit některou z databází databázi typu klíč-hodnota, která neuvažuje strukturu hodnot a byla by tedy pro
naše potřeby efektivnější.

## Návrh a implementace aplikace

Pro implementaci s použitím databáze MongoDB jsme se rozhodli použít jazyk Python s knihovnou `pymongo`. Jelikož se v podstatně pracuje 
s dynamickými strukturovanými daty, nedávalo příliš smysl volit nějaký silně typovaný jazyk jako Java, kde se běžně definují předem dané datové modely. 
Python je vhodným kandidátem především díky jednoduchosti použití a dobré podpoře dynamických slovníků a seznamů, ve kterých lze celá data snadno reprezentovat a 
předávat databázovém driveru. 

Aplikaci dává smysl rozdělit na dva Python skripty, jeden starající se o stahování dat, jejich zpracování a uložení v databázi, a druhý
provádějící samotné vyhledávání spojů. 

Pro stažení dat je využit balíče `wget`, pro jejich parsování `xmltodict` a pro jejich vkládání a dotazování `pymongo`.

V rámci implementace na vyhledávaní spojů jsme se soustředili na vyhledání nejbližšího možného spoje v daný den podle zadaného času.
Na výstup jsou vypsány všechny zástávky od zadané počáteční do zadané cílové stanice společně s časy zastávek.

Aplikační kód zpracovává data, daná referencemi jako klíč do hlavní kolekce, které byli získany průnikem v pomocné kolekci. 
Nad těmito nezpracovanými daty zjišťuje zda je daný spoj validní a postupně filtruje jednotlivé spoje. 
Nejdříve se zjištuje zda je v zadaném datumu spoj aktivní. Dále jsou seskupeny všechny stanice, kde spoj zastavuje a ověřeno, že spoj 
projíždí ve správném směru a zastavuje na zadaných stanicích. Nakonec je ověřen čas odjezdu a porovnán s přechozími spoji, zdali se nejedná o dřívějši spoj.

## Způsob použití 

Pro použití aplikace je nutné mít na lokálním stroji nainstalovanou a spuštěnou databázi `MongoDB`. 
Oba skripty se připojují k databází s výchozím hostname (localhost) a portem (27017).
Dále je třeba mít nainstalovaný Python 3.8 nebo vyšší a potřebné moduly uvedené v `requirements.txt`.

Skript pro načítání dat se spustí bez parametrů příkazem `python3 upa-import.py`, výsledkem je připravená databáze s daty.

Skript pro dotazování na spoje je třeba pouštět se čtyřmi parametry: `python3 upa-cli.py <zdrojová stanice> <cílová stanice> <datum> <cas>`.

Příklady korektního vyhledávání: 
- `python3 upa-cli.py "Ostrava hl.n." "Praha hl. n." 2021-12-18 10:44`
- `python3 upa-cli.py "Karlovy Vary" "Nebanice" 2021-12-18 4:44`

## Experimenty

Testování bylo prováděno na celém datovém balíku roku 2022, v databázi se tak po importu vyskytuje přes 12000 zpráv o spojích a v pomocné
kolekci přes 3000 záznamu s lokacemi. Celý import dat, to znamená stažení, parsování, zpracování a uložení do db trvalo na 
testovacím stroji zhruba 74 sekund. Rychlost přidání zprávy se pohybuje pod setinou sekundy. 

Testování vyhledávání proběhlo na několika variantách lokací a časů, jak demonstruje tabulka níže. Co se týče
rychlosti vyhledávání spojů, tak tam považujeme aplikaci za excelující, což je dáno především přístupem přes klíče. Sice je většina
zpracování a dohledánvání prováděna dále v aplikačním kódu, ale tento kód už operuje nad relativně malým množstvím záznamů. 

| odkud         | kam          | kdy              | **čas vyhledání** |
|---------------|--------------|------------------|-------------------|
| Ostrava hl.n. | Praha hl. n. | 2021-12-18 10:44 | **0.0214s**       |
| Karlovy Vary  | Nebanice     | 2021-12-18 4:44  | **0.0347s**       |
| Mirošov město | Rokycany     | 2022-05-22 14:22 | **0.0104s**       |
| Dlouhé Dvory  | Turnov       | 2022-05-24 11:00 | **0.0121s**       |

