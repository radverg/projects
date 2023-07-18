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
  
  
  $browserAddress = ltrim($_SERVER['REQUEST_URI'], "/");
  $browserAddress = trim($browserAddress);
  $addressArray = explode("/",$browserAddress);
  
  if (empty($addressArray[0]))
  {
      require_once 'mainpage.php';
  }
  else if ($addressArray[0] == "introduction" || $addressArray[0] == "manual" || $addressArray[0] == "contact" || $addressArray[0] == "versions_downloads")
  {
      require_once 'mainpage.php';
  }
  else if ($addressArray[0] == "play")
  {
      require_once 'online/index.html';
  }
  else if ($addressArray[0] == "editor")
  {
      require_once 'online/editor.html';
  }
  
  
  
 else {
      header("HTTP/1.1 301 Moved Permanently");
      header("Location: http://tankhunt.wz.cz");
      header("Connection: close");
}
  
  
  
  ?>

  
    