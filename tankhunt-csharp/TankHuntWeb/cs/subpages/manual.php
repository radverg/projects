

<article id="controls">
        <header>
          <h2> Ovládání: </h2>
        </header>
        <section>
           <ul>
            <li>Šipka nahoru - pohyb dopředu.</li>
            <li>Šipka dolů - pohyb dozadu.</li>
            <li>Šipky doleva a doprava - otáčení tanku a hlavně.</li>
            <li>S - výstřel základní střely.</li>
            <li>D - výstřel speciální střely (sebrané z náhodného místa v bludišti).</li>
            <li>F - zapnutí neviditelnosti.</li>
            <li>Mezerník - vygenerování nového bludiště (pouze pro hostitele serveru).</li>
           </ul>
        </section>           
      </article>
                       
      <article id="setupserver">
        <header>
          <h2> Jak správně nastavit server: </h2>
        </header>
        <section>
           <p>Můžete vytvořit server a říct kamarádům, aby se připojili, ale vytváření serveru, na který se lidé mohou připojit, požaduje nastavení 2 věcí:</p>
           <p>1) Potřebujete povolit Tank hunt ve windows firewall. Windows se vás při vytváření serveru většinou zeptá, jestli chcete aplikaci povolit. Pokud se tak nestane, řiďte se následujícími kroky: </p>
           <p>
           - Otevřete ovládací panely. <br>
           - Vyberte &quotBrána Windows firewall.&quot <br>
           - Na levé straně okna klikněte na odkaz &quotPovolit aplikaci nebo funkci průchod bránou Windows Firewall&quot. <br>
           - V seznamu najděte Tank hunt a zatrhněte hned vedle názvu <br>
           </p>
           <p>2) Potřebujete povolit port na vaší lokální ip adrese, potom se hráči budou moci připojit přes vaší externí ip. Toto neplatí pro hraní přes lan. </p>
           <p>
            - Stiskněte klávesy windows + R   <br>
            - Napište &quotcmd&quot a klikněte ok. <br>
            - Sem napiště &quotipconfig&quot a stiskněte enter.<br>
            - Najděte default gateway adresu, napište ji jako odkaz do vašeho prohlížeče a stiskněte enter. <br>
            - Napište uživatelské jméno a heslo (měli byste tyto údaje znát, pokud ne, vyzkoušejte standartní údaje: jméno: &quotadmin&quot a heslo: &quotadmin&quot). <br>
            - Najděte zde tyto tlačítka: Port forwarding (přesměrování portů), Virtual server (virtuální server), NAT (každý routerto má jiné). Klikněte na to. <br>
            - Tady nastavte váš port pro server a vaši lokální ip adresu, kterou můžete nalázt v cmd (Ipv4 address). Vyberte UDP protocol.<br>
            - To je vše, hráči se nyní mohou připojit, pokud do kolonek napíší vaši externí ip adresu a vámi zvolený port. Více deatilů a informací můžete najít zde: <a target="_blank" href="http://www.wikihow.com/Set-Up-Port-Forwarding-on-a-Router">http://www.wikihow.com/Set-Up-Port-Forwarding-on-a-Router</a>
            </p>                     
        </section>         
      </article>