
<!DOCTYPE HTML>
<?php
  $settings = Array(
      PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
      PDO::MYSQL_ATTR_INIT_COMMAND => "SET NAMES utf8"
  );
  
  $connnection = new PDO("mysql:host=sql2.webzdarma.cz;dbname=tankhunt.wz.5620", 'tankhunt.wz.5620', 'xrslkfdslowerl156gsdfa', $settings);
  
  $query_check = $connnection->prepare("SELECT COUNT(*) AS numberips FROM visitors WHERE ip = ?");
  $query_check->execute(array($_SERVER['REMOTE_ADDR'])); 
  if ($query_check->fetch()['numberips'] == 0)
  {
      $query = $connnection->prepare("INSERT INTO visitors (ip, visits) VALUES (? ,?)");
        $parameters = array($_SERVER['REMOTE_ADDR'], 1);
        $query->execute($parameters);
  }
      
  
  
  
  
  ?>  
  <html>
    <head>
      <title>Tank hunt - Oficiální stránka</title>
      <meta charset="UTF-8">
      <meta name="author" content="Veverka-Radek">
      <meta name="description" content="2d multiplayer tank fighting game">
      <meta name="keywords" content="tank, tanks, hunt, multiplayer, 2d, game, fight, fighting, free, xna, programming, microsoft, .net">      
      <link rel="stylesheet" type="text/css" href="../styles.css">
      <link rel="shortcut icon" href="images/circle_tank_favicon.png">       
    </head>
    
    <body>    
    <header>
     
        <img src="../images/circle_tank_web_logo.png" class="logo"> 
        <h1>Tank hunt - Oficiální stránka</h1>  
        <a href="index.php"><img width="32px" height="21px" src="../images/czech_flag.png" align="right" border="0"></a>
        <a href="../"><img width="32px" height="21px" src="../images/english_flag.png" align="right" border="0"></a>
      <div class="cleaner"></div>
    
      <nav>
          <ul>
               <?php
              $page;
              if (isset($_GET['page']))
              {
                  $page = $_GET['page'];
              }
              else
              {
                  $page = 'introduction';
              }              
              ?>
              <li><a <?php if ($page == 'introduction') { echo 'class="selected"';} ?> href="index.php?page=introduction">Úvod</a></li>
              <li><a <?php if ($page == 'manual') { echo 'class="selected"';} ?> href="index.php?page=manual">Návody</a></li>
              <li><a <?php if ($page == 'versions_downloads') { echo 'class="selected"';} ?> href="index.php?page=versions_downloads">Verze & Stažení</a></li>
              <li><a <?php if ($page == 'contact') { echo 'class="selected"';} ?> href="index.php?page=contact">Kontakt</a></li>
              
          </ul>                    
      </nav>
      
      
      <br>
    </header>
        <?php
            if (isset($_GET['page']))
            {
                $file_path = "subpages/" . $_GET['page'] . ".php";
                if (file_exists($file_path))
                {
                    require($file_path);
                }
                else
                {
                    echo 'No content found!';
                }
            }
            else
            {
                $filepath = "subpages/introduction.php";
                require($filepath);
            }
        ?>
        
        
    </body>
  </html>
    
       ¨