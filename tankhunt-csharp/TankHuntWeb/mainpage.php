<html>
    <head>
      <title>Tank hunt - Official webpage</title>
      <meta charset="UTF-8">
      <meta name="author" content="Veverka-Radek">
      <meta name="description" content="2d multiplayer tank fighting game">
      <meta name="keywords" content="tank, tanks, hunt, multiplayer, 2d, game, fight, fighting, free, xna, programming, microsoft, .net">      
      <link rel="stylesheet" type="text/css" href="styles.css">
      <link rel="shortcut icon" href="images/circle_tank_favicon.png">       
    </head>
    
    <body>    
    <header>
     
        <img src="images/circle_tank_web_logo.png" class="logo"> 
        <h1>Tank hunt - Multiplayer game website</h1>  
        <a href="cs/index.php"><img width="32px" height="21px" src="images/czech_flag.png" align="right" border="0"></a>
        <a href=""><img width="32px" height="21px" src="images/english_flag.png" align="right" border="0"></a>
      <div class="cleaner"></div>
    
      <nav>
          <ul>
              <?php
              $page;
               $browserAddress = ltrim($_SERVER['REQUEST_URI'], "/");
                $browserAddress = trim($browserAddress);
                $addressArray = explode("/",$browserAddress);
  
                if (empty($addressArray[0]))
                {
                    $page = "introduction";
                }
                else if ($addressArray[0] == "introduction" || $addressArray[0] == "manual" || $addressArray[0] == "contact" || $addressArray[0] == "versions_downloads")
                 {
                    $page = $addressArray[0];
                 }
  
              ?>
              <li><a <?php if ($page == 'introduction') { echo 'class="selected"';} ?> href="introduction">Introduction</a></li>
              <li><a <?php if ($page == 'manual') { echo 'class="selected"';} ?> href="manual">Manual</a></li>
              <li><a <?php if ($page == 'versions_downloads') { echo 'class="selected"';} ?> href="versions_downloads">Versions & Downloads</a></li>
              <li><a <?php if ($page == 'contact') { echo 'class="selected"';} ?> href="contact">Contact</a></li>
          </ul>                    
      </nav>
      
      <br>
    </header>
        <?php      
                $file_path = "subpages/" . $page . ".php";
                if (file_exists($file_path))
                {
                    require($file_path);
                }
                else
                {
                    echo 'No content found!';
                }
        ?>
    </body>
  </html>

