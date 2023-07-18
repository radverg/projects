$(function(){

$("#recBtn").click(function() {
        recountFromPattern();
    });

$(document).disableSelection().on("contextmenu", function(e) { return false; })
        .on("dragstart", "div", function() { return false; })
        ;

$("#tbSquare").bind("input", function() {
    squareSize = $(this).val();
        recountPattern();
});

$("#hider").click(function() {
    if ($("#hider").text() === "hide") {
        $("#inputOutput").animate({ opacity: "0" }, 400, function() {
      $("#hider").text("show");
      $("#inputOutput").hide();
    });
    }
    else {
       // $("#hider").after($(inputOutput));
       $("#inputOutput").show();
        $("#hider").text("hide");
        $("#inputOutput").animate({ opacity: "1" }, 400);
    }
        
    }
    );

var repositionInput = function () { $("#inputOutput").css("left", addPx(innerWidth - removePx($("#inputOutput").css("width")) - 50));
                                    $("#hider").css("left", addPx(innerWidth - removePx($("#hider").css("width")) - 50));};
window.onresize = repositionInput;
repositionInput();

var squareSize = 150;

var sizeX;
var sizeY;

var wallThickness = 6;
var squareElements = new Array();
recountFromPattern();

var Rectangle = function(x, y, sizeX, sizeY)
{
    var self = this;
    this.x = x;
    this.y = y;
    this.sizeX = sizeX;
    this.sizeY = sizeY;
};

function recountFromPattern()
{
    $("div[data-square]").remove();
    var pattern = ($("textarea").val()).split(" ");
    squareSize = parseInt(pattern[1]);
    sizeX = parseInt(pattern[2]);
    sizeY = parseInt(pattern[3]);
        for (var x = 0; x < sizeX; x++) {
            squareElements[x] = new Array();
        }
        
        for (var x = 0; x < sizeX; x++) { // ! x should be -1
            for (var y = 0; y < sizeY; y++) { // ! y should be -1
                squareElements[x][y] = $('<div data-square></div>').css("top", addPx(y * squareSize))
                        .css("left", addPx(x * squareSize))
                        .css("width", addPx(squareSize))
                        .css("height", addPx(squareSize))
                        .mouseover(function(){
                            $(this).css("background-color", "ivory").css("z-index", "0");
                        }
                        ).mouseleave(function() { $(this).css("background-color", "white").css("z-index", "0");})
                        .mousedown(function(e) {
                            if (e.which === 1) { // Left mouse button
                               var children = $(this).children();
                                for (var i = 0; i < children.length; i++) {
                                   if (children[i].style.width === addPx(squareSize + wallThickness)) {
                                       $(children[i]).remove();
                                        recountPattern();
                                       return;
                                   }
                               }
                                  $(this).append(createWallElement(addPx(squareSize + wallThickness), addPx(wallThickness)));     
                                recountPattern();
                            }
                            else if (e.which === 3) { // Right mouse button
                                var children = $(this).children();
                                for (var i = 0; i < children.length; i++) {
                                   if (children[i].style.height === addPx(squareSize + wallThickness)) {
                                       $(children[i]).remove();
                                        recountPattern();
                                       return;
                                   }
                               }
                                  $(this).append(createWallElement(addPx(wallThickness), addPx(squareSize + wallThickness)));     
                                recountPattern();
                            }
                        });
                $("body").prepend(squareElements[x][y]);
            }
        }
    for (var i = 4; i < pattern.length; i+=3) {
        var rect = new Rectangle();
        rect.x = parseInt(pattern[i]);
        rect.y = parseInt(pattern[i + 1]);
        if (parseInt(pattern[i + 2]) === 0)
        {
            rect.sizeX = squareSize + wallThickness;
            rect.sizeY = wallThickness; 
        }
        else
        {
            rect.sizeX = wallThickness;
            rect.sizeY = squareSize + wallThickness; 
        }
        $(squareElements[rect.x][rect.y]).append($("<div data-wall></div>")
                        .css("width", addPx(rect.sizeX))
                        .css("height", addPx(rect.sizeY))
                        .css("background-color", "black"));      
    }           
}

function createWallElement(sizeX, sizeY)
{
    return $("<div data-wall></div>")
                        .css("width", sizeX)
                        .css("height", sizeY)
                        .css("background-color", "black");
}

function recountPattern()
{
    var result = "1 " + squareSize.toString() + " " + sizeX.toString() + " " + sizeY.toString();
    var divs = $("div[data-square] div");
        for (var i = 0; i < divs.length; i++) {
            result = result + " " + (parseInt(removePx(divs[i].parentElement.style.left)) / squareSize).toString() + " " +  (parseInt(removePx(divs[i].parentElement.style.top)) / squareSize).toString();
            if (parseInt(removePx(divs[i].style.width)) > parseInt(removePx(divs[i].style.height))) { // Horizontal case
                result += " 0";
            }
            else {
                result += " 1";
            }
        }
    $("textarea").val(result);
}

function addPx(value)
{
    return value + "px";
}

function removePx(value)
{
    return value.substring(0, value.length - 2);
}    
});


