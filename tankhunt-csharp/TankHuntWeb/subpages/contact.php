

<article id="contact">
        <header>
          <h2> Contact me: </h2>
        </header>
        <section>
            <form method="post">
                <table style="border-spacing: 10px">
                    <tr>
                        <td>Your name: </td>
                        <td><input type="text" name="name"></td>
                    </tr>
                     <tr>
                        <td>Your email: </td>
                        <td><input type="email" name="email"></td>
                    </tr>
                </table>
                <textarea name="message"></textarea><br>
                <input type="submit" value="Send" />
            </form>
            
            <?php
            if ($_POST)
            {
                if (isset($_POST['name']) && $_POST['name'] && isset($_POST['email']) && $_POST['email'] && isset($_POST['message']) && $_POST['message'])
                {
                    $mailhead = "From: " . $_POST['name'];
                    $mailhead .= "\nMIME-Version 1.0\n";
                    $mailhead .= "Content-Type: text/html; charset=\"utf-8\"\n";
                    $result = mail('radek.veverka@email.cz', 'TankHunt mailform', $_POST['message'] .
                            "\n\n From: " . $_POST['name'] . " - " . $_POST['email'], $mailhead);
                    
                    
                    if ($result)
                    {
                        echo 'Message has been succesfully sent!';
                    }
                    else
                    {
                        echo 'Failed to send message! Check email address.';
                    }
    
                }
                else
                {
                    echo 'Invalid input!';
                }
            }
             ?>
        
        </section>           
</article>


