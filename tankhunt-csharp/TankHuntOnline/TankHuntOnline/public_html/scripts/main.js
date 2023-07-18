
var canvas;
var context;
var level;
var connection;
var tankImage;
var playerSprite;
tankImage = new Image();
tankImage.src = "images/circle_tank.png"
var players = new Array();
var myID;
var centerScreen;

// Declare objects
var SquareTile = function(x, y, squareSize)
{
    this.pos = new Vector2(x, y);
    this.totalPos = new Vector2(squareSize * x, squareSize * y);
    this.squareSize = squareSize;
   
    this.elements = new Array();
}


var Rectangle = function(x, y, sizeX, sizeY)
{
    var self = this;
    this.x = x;
    this.y = y;
    this.sizeX = sizeX;
    this.sizeY = sizeY;
};

var Level = function(pattern)
{
    var self = this;
    this.rectangles = new Array();  
    
    var levelArray = pattern.split(" ");
    
    this.squareSize = parseInt(levelArray[1]);
    this.sizeX = parseInt(levelArray[2]);
    this.sizeY = parseInt(levelArray[3]);
    this.totalSize = new Vector2(this.sizeX * this.squareSize, this.sizeY * this.squareSize);
    this.wallThickness = 5;
    this.tiles = new Array();
    for (var x = 0; x < this.sizeX; x++) {
        this.tiles[x] = new Array();
        for (var y = 0; y < this.sizeY; y++) {
            this.tiles[x][y] = new SquareTile(x, y, self.squareSize);
        }
    }
    
    for (var i = 4; i < levelArray.length; i+=3) {
        var rect = new Rectangle();
        rect.x = parseInt(levelArray[i]);
        rect.y = parseInt(levelArray[i + 1]);
        if (parseInt(levelArray[i + 2]) === 0)
        {
            rect.sizeX = this.squareSize;
            rect.sizeY = this.wallThickness; 
        }
        else
        {
            rect.sizeX = this.wallThickness;
            rect.sizeY = this.squareSize; 
        }
        
        this.rectangles.push(rect);
        this.tiles[rect.x][rect.y].elements.push(rect);
    }
};
Level.prototype.draw = function()
{
    var minTile = new Vector2(Math.floor((ViewPort.x / this.squareSize)) - 1, Math.floor((ViewPort.y) / this.squareSize) - 1);
    var maxTile = new Vector2(Math.floor((ViewPort.x + ViewPort.width) / this.squareSize) + 1, Math.floor((ViewPort.y + ViewPort.height) / this.squareSize) + 1);
    for (var x = minTile.x; x <= maxTile.x; x++) {
        for (var y = minTile.y; y <= maxTile.y; y++) {
            
            var realTile = new Vector2(x, y);
            if (x < 0)
                realTile.x += this.sizeX;
            else if (x > this.sizeX - 1)
                realTile.x -= this.sizeX;
            
            if (y < 0)
                realTile.y += this.sizeY;
            else if (y > this.sizeY - 1)
                realTile.y -= this.sizeY;
            
            
            for (var el = 0; el < this.tiles[realTile.x][realTile.y].elements.length; el++) {
                 var rectCurrent = this.tiles[realTile.x][realTile.y].elements[el];
                 var delta = new Vector2((realTile.x - x) * this.squareSize, (realTile.y - y) * this.squareSize);
                    context.fillRect(((rectCurrent.x * this.squareSize) - delta.x) - ViewPort.x, (((rectCurrent.y) * this.squareSize) - delta.y) - ViewPort.y, rectCurrent.sizeX, rectCurrent.sizeY);
            }
           
        }
    }
    
    
};

var Sprite = function(texture)
{
    this.texture = texture;
    this.rect;
    this.ID;
    this.pos;
    this.currentRect = new Rectangle(0, 0, 0, 0);
    this.rot;
    this.velocity;
};

Sprite.prototype.draw = function()
{
 
        context.save();
        context.translate(this.rect.x + (this.rect.sizeX / 2), this.rect.y + (this.rect.sizeY / 2));
        context.rotate(this.rot);
        context.drawImage(this.texture, -(this.rect.sizeX / 2), -(this.rect.sizeY / 2), this.rect.sizeX, this.rect.sizeY);
        context.restore();
    
};

Sprite.prototype.interpolate = function(){
    var deltaX = this.rect.x - this.currentRect.x ;
    var deltaY = this.rect.y - this.currentRect.y;
    this.currentRect.x += (deltaX * 0.5);
    this.currentRect.y += (deltaY * 0.5);
    clampPos(this.currentRect, level.totalSize);
};

Sprite.prototype.drawTo = function (pos)
{
    context.save();
        context.translate(pos.x + (this.rect.sizeX / 2), pos.y + (this.rect.sizeY / 2));
        context.rotate(this.rot);
        context.drawImage(this.texture, -(this.rect.sizeX / 2), -(this.rect.sizeY / 2), this.rect.sizeX, this.rect.sizeY);
        context.restore();
};

var Vector2 = function(x, y)
{
    this.x = x;
    this.y = y;
};

var ViewPort = {
    x: 0,
    y: 0,
    width: 0, // This is equal to canvas width
    height: 0,  // This is equal to canvas height
    recount: function(player)
    {
        this.x = player.currentRect.x - (this.width / 2) + player.rect.sizeX / 2;
        this.y = player.currentRect.y - (this.height / 2) + player.rect.sizeY / 2;
    }
};



// ----------------


var init = function()
{
    document.body.style.overflow = "hidden";
    document.body.style.margin = "0px";
    
    canvas = document.getElementById("gameCanvas");
    ctx = canvas.getContext("2d");
    
    connection = new WebSocket("ws://90.180.196.121:55555");
    
    connection.onopen = function()
    {
        connection.send("2"); // Send information that handshake is completed - request initial data (level, players etc.)
        mainLoop();
    };
    
    connection.onmessage = function(e)
    {
        switch (e.data[0])
        {
            case "1": // Level update
            {
                level = new Level(e.data);
                break;
            }
            case "4": // Soft players update
            {
                var split = e.data.split(" ");
                for (var i = 1; i < split.length; i+=4) {
                    var id = parseInt(split[i]);
                    if (!players[id])
                    {
                        players[id] = new Sprite(tankImage);
                        players[id].rect = new Rectangle(0, 0, 40, 40);
                    }

                    players[id].rect.x = parseFloat(split[i + 1].replace(",", "."));
                    players[id].rect.y = parseFloat(split[i + 2].replace(",", "."));
                    clampPos(players[id].rect, level.totalSize);
                    players[id].rot = parseFloat(split[i + 3].replace(",", "."));
                }
                ViewPort.recount(players[myID]);
                break;
                
            }
            case "5": // Id update
            {
                myID = parseInt(e.data.split(" ")[1]);
            }
                
        }
       
    };
    
    canvas = document.getElementById("gameCanvas");
    resizeCanvas();
    context = canvas.getContext("2d");
   
};


window.onkeydown = function(e)
{
    connection.send("3 " + getKeyName(e.keyCode) + " 1");
};

window.onkeyup = function(e)
{
    connection.send("3 " + getKeyName(e.keyCode) + " 0");
};

window.onresize = function ()
{
    // Resize and reposition canvas
    resizeCanvas();
};

window.onload = init;

var resizeCanvas = function()
{
    var innerW = innerWidth - 10;
    var innerH = innerHeight - 10;
    canvas.width = innerW;
    canvas.height = (innerW / 16) * 9;
    
    if (canvas.height > innerH)
    {
        canvas.height = innerH;
        canvas.width = ((innerH / 9) * 16);
    }
    ViewPort.width = canvas.width;
    ViewPort.height = canvas.height;
    canvas.style.top = ((innerHeight - canvas.height) / 2).toString() + "px";
    canvas.style.left = ((innerWidth - canvas.width) / 2).toString() + "px";
    centerScreen = new Vector2(canvas.width / 2, canvas.height / 2);
};

// Setup request anim frame
window.requestAnimationFrame = (function() {
        return (window.webkitRequestAnimationFrame ||
            window.mozRequestAnimationFrame || 
            window.oRequestAnimationFrame ||
            window.msRequestAnimationFrame ||
            function( /* function FrameRequestCallback */ callback, /* DOMElement Element */ element) {
                window.setTimeout(callback, 1000 / 60);
        });

    })();
    
    
    function mainLoop()
    {
        requestAnimationFrame(mainLoop);
        update();
        draw();
    }
    
    function draw(deltaMiliseconds)
    {
        context.clearRect(0, 0, canvas.width, canvas.height);
        level.draw();
        for (var i = 0; i < players.length; i++) {
            if (i === myID) {
                players[i].drawTo(new Vector2(centerScreen.x - players[i].rect.sizeX / 2, centerScreen.y - players[i].rect.sizeY / 2));
            }
            else {
                players[i].draw();
            }
    }
    }
    
    function update(deltaMiliseconds)
    {
       // players[0].interpolate();
       players[0].currentRect.x = players[0].currentRect.x + 3;
       clampPos(players[0].currentRect, level);
    }
    
    function getKeyName(keyCode)
    {
        switch (keyCode)
        {
            case 37:
                return "Left";
            case 38:
                return "Up";
            case 39:
                return "Right";
            case 40:
                return "Down";
        }
    }
    
    function clampPos(pos, levelSize)
    {
        if (pos.x < 0)
            pos.x += levelSize.x;
        else if (pos.x > levelSize.x)
            pos.x -= levelSize.x;
        
        if (pos.y < 0)
            pos.y += levelSize.y;
        else if (pos.y > levelSize.y)
            pos.y -= levelSize.y;
        
    }
    
    