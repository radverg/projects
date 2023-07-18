<article id="Chat commands">
        <header>
          <h2> Chat commands (available in upcoming version 1.8):</h2>
        </header>
        <section>
            <p>All chat commands starts with minus char '-' and all of them are not shown as chat messages to other players .
            All players are determined with specific id shown in brackets behind their names on panel. </p>
           <ul>
            <li>Set darkness percentage: -set darkness to (value 0 - 100) Example: -set darkness to 52</li>
            <li>Set level size (square count): -set minmaxsize to (minsize)-(maxsize) Example: -set minmaxsize to 10-15 </li>
            <li>Set square size: -set minmaxsquare to (minsize)-(maxsize) Example: -set minmaxssquare to 110-165</li>
            <li>Reset statistics: -reset stats to (players) Example: -reset stats to 1-3-5 (this restarts statistics to players with IDs 1, 3 and 5)</li>
            <li>Set player's color: -set color to (players) to (R-G-B) Example: -set color to 2 to 255-0-0 (sets color to player with id 2 to red).
            You can use color's name: -set color to (players) to (colorname) Example: -set color to all to lightblue</li>
            <li>Kick players from server: -kick (players) Example: -kick 1-4 (kicks players with IDs 1 and 4)</li>
            <li>Kill players: -kill (players) Example: -kill all (kills all players - unexpectedly)</li>
            <li>Turn on/off tank autorotating: -autorotating true/false Example: -autorotating true (turns on autorotating)</li>
            <p>That is all for now, I will be adding new commands regularly.</p>
           </ul>
        </section>           
      </article>


<article id="controls">
        <header>
          <h2> Default controls: </h2>
        </header>
        <section>
           <ul>
            <li>Arrow up key - move forward.</li>
            <li>Arrow down key - move backwards.</li>
            <li>Arrow right and arrow left key - rotate tank and change it's direction.</li>
            <li>S key - shoot by one-shot-cannon.</li>
            <li>D key - shoot by advanced weapon (collected from randomly generated items)</li>
            <li>SPACE - Generate new level - server only</li>
            <li>F key - Start invisibility.</li>
           </ul>
        </section>           
      </article>
      <article id="setuphamachiserver">
        <header>
          <h2> Hamachi - the easiest way to create server and play with remote friends: </h2>
        </header>
        <section>
            <li>Download Hamachi (for example from official website: <a href="https://secure.logmein.com/products/hamachi/download.aspx">https://secure.logmein.com/products/hamachi/download.aspx)</a></li>
            <li>Create your virtual private network, send username and password to friends and let them connect there.</li>
            <li>Create server with some port in Tank hunt, others will use that port and instead of ip address they will write &quotlocalhost&quot</li>
        </section>           
      </article>

      <article id="setupserver">
        <header>
          <h2> How to create server with port forwarding: </h2>
        </header>
        <section>
           <p>You can create a server and tell your friends to connect there, but creating connectable server
           requires two setups below.</p>
           <p>1) You need to allow your application in the windows firewall. Windows will usually ask you to this while creating the server, but if it doesn't,
           follow this: </p>
           <p>
           - Open the control panel. <br>
           - Select &quotSystem and security.&quot <br>
           - Under Windows firewall link, clik on sublink &quotAllow an app through Windows firewall&quot. <br>
           - Click on &quotChange setting&quot, then on &quotChoose another app&quot and find TankHunt.exe. <br>
           </p>
           <p>2) You need forward used port to your local ip address, after that, unlocal players can connect through your external ip. This is not required for lan play. </p>
           <p>
            - Press Winkey + R   <br>
            - Type there &quotcmd&quot and run it. <br>
            - Write &quotipconfig&quot to command line and press enter.<br>
            - Find default gateway address, write it into address line in your internet browser. <br>
            - Insert your router username and password (you should know it, if you don't, try defaults). <br>
            - Find for these buttons: Port forwarding, Virtual server, NAT (each router has different one). Click on it. <br>
            - There set your port for server and your local ip address, which you can also find in that cmd (Ipv4 address). Select UDP protocol.<br>
            - That's it, players now will be able to connect to your server if they use your external ip. You can find more detailed information here: <a target="_blank" href="http://www.wikihow.com/Set-Up-Port-Forwarding-on-a-Router">http://www.wikihow.com/Set-Up-Port-Forwarding-on-a-Router</a>
            </p>                     
        </section>           
      </article>