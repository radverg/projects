/**
 * Created by Radek on 06.01.2017.
 */

var $grabbedElement = null; // Drag and drop element keeper
var previousMouseMovePos = { x: 0, y: 0};
var mapPreviewCanvas = document.createElement("canvas");
var mapPreviewCtx = mapPreviewCanvas.getContext("2d");


var sources = {
    ensk: new Image(),
    lakeville: new Image(),
    ruinberg: new Image(),
    erlenberg: new Image(),
    himmelsdorf: new Image(),
    doly: new Image(),
    arktida: new Image(),
    karelie: new Image(),
    posvatneUdoli: new Image()

}

function createTable(rows, columns) {
    var $table = $("<table></table>");
    var cellID = 1;


    $("body").append($table);
}

$(document).mousemove(function (e) { // Drag and drop functionality
    if ($grabbedElement !== null) {
        $($grabbedElement).css("left", ($($grabbedElement).offset().left + e.pageX - previousMouseMovePos.x).toString() + "px")
            .css("top", ($($grabbedElement).offset().top + e.pageY - previousMouseMovePos.y).toString() + "px");
    }

    previousMouseMovePos.x = e.pageX;
    previousMouseMovePos.y = e.pageY;
});

function splitPictureToPuzzles(countX, countY, image) {
    var canvas = document.createElement("canvas");

    var context = canvas.getContext("2d");
    var square = image.width / countX;
    canvas.width = square;
    canvas.height = square;
    var cellID = 1;
    for (var x = 0; x < countX; x++) {
        for (var y = 0; y < countY; y++) {
            context.clearRect(0, 0, canvas.width, canvas.height);
            context.drawImage(image, x * square, y * square, square, square, 0, 0, canvas.width, canvas.height);



            $("body").append($("<img>").attr("src", canvas.toDataURL())
                .attr("data-id", cellID)
                .css("position", "absolute")
                .css("z-index", 1)
                .css("border", "2px solid orange")
                .mousedown(function() {
                    $grabbedElement = $(this);
                    if ($(this).parent("div").length === 1) { // Means that we need to reset the border of the board
                        $(this).parent().css("border-width", "1px");
                    }
                    $(this).css({"width": "", "height": "", "top": $(this).offset().top + "px", "left": $(this).offset().left + "px", "border-width": "2px"});
                    $(this).detach();
                    $("body").append($(this));


                    

                })
                .mouseup(function () {
                    $grabbedElement = null;
                    // here goes logic for putting image to board
                    var centerPos = { x: $(this).offset().left + $(this).outerWidth() / 2, y: $(this).offset().top + $(this).outerHeight() / 2};
                  //  alert($(this).offset().left);
                    var dx = centerPos.x - $("#board").offset().left;
                    var dy = centerPos.y - $("#board").offset().top;
                    if (dx < 0 || dx > $("#board").offset().left + $("#board").width()
                        || dy < 0 || dy > $("#board").offset().top + $("#board").height())
                        return;

                    var squareSize = $("#board div").width();

                    var boardX = Math.floor(dx / squareSize);
                    var boardY = Math.floor(dy / squareSize);
                    $(this).css({ "top": "0px", "left": "0px", "border-width" : "0px", "width": "100%", "height": "100%"});
                    $(this).detach();
                    $("#board div[data-x='" + boardX + "'][data-y='" + boardY + "']").css("border-width", "0px").append($(this));

                })
                .mouseover(function () {
                    $(this).css("z-index", 2).css("border-color", "red");

                })
                .mouseleave(function () {
                    $(this).css("z-index", 1).css("border-color", "orange");
                }));

            cellID++;
        }

    }
}

function createPuzzleBoard(countX, countY, squareSize) {
    var $container = $("<div></div>").css("border", "3px solid red").css("width", squareSize * countX).css("height", squareSize * countY)
        .css("position", "absolute");

    for (var x = 0; x < countX; x++) {
        for (var y = 0; y < countY; y++) {
           var newDiv = $("<div></div>")
               .css({"position" : "absolute", "width" : squareSize + "px", "height" : squareSize + "px",
                    "top" : (squareSize * y).toString() + "px", "left" : (squareSize * x).toString() + "px",
                   "border" : "1px solid gray", "z-index" : 0})
               .attr("data-x", x)
               .attr("data-y", y);
            $container.append(newDiv);
        }
    }
    return $container;

}

function setRandomPosToPuzzles($images, startX, startY) {
    $($images).each(function () {
       var randomX = $(window).width() * Math.random();
        var randomY = (randomX > startX) ? $(window).height() * Math.random() : startY + ($(window).height() - startY) * Math.random();
        $(this).css({"top": randomY + "px", "left" : randomX + "px"});
    });
}

$(function() {
    $(document).on("dragstart", function () {
        return false;
    })
        .on("contextmenu", function () {
            return false;
        });

    $("#mapList div").mouseover(function () {
        var that = this;
       $(this).animate({"width": 100 + "%"}, 150);

        // Set up and show the preview
        var pos = { x: ($(that).outerWidth() / 85 * 100) + $(that).offset().left - 12, y: $(that).offset().top + $(that).outerHeight() / 2 - $(mapPreviewCanvas).height() / 2 };
        if ($("#mapList").offset().top > pos.y) pos.y = $("#mapList").offset().top;
        if ($("#mapList").offset().top + $("#mapList").outerHeight() < pos.y + $(mapPreviewCanvas).outerHeight()) pos.y = $("#mapList").offset().top + $("#mapList").outerHeight() - $(mapPreviewCanvas).outerHeight();

        $(mapPreviewCanvas).css({"left" : (pos.x).toString() + "px",
            "top": (pos.y).toString() + "px"});
        mapPreviewCtx.clearRect(0, 0, mapPreviewCanvas.width, mapPreviewCanvas.height)
        mapPreviewCtx.drawImage(sources[$(that).attr("data-map")], 0, 0, 400, 400);
        $(mapPreviewCanvas).stop().fadeIn(150);
    });

    $("#mapList div").mouseleave(function () {
       $(this).animate({"width": 85 + "%"}, 150);
        $(mapPreviewCanvas).stop().fadeOut(150);
    });

    $("#mapList div").click(function () {
       $("#mapList, #settings").hide();
        var count = $("input[name=difficulty]:checked").val();
        splitPictureToPuzzles(count, count, sources[$(this).attr("data-map")]);
        $("body").append(createPuzzleBoard(count, count, Math.floor(sources[$(this).attr("data-map")].width / count)).attr("id", "board"));
        setRandomPosToPuzzles($("body img"), $("#board").offset().left + $("#board").width(), $("#board").offset().top + $("#board").height());
    });

    // Load up the images
    var keys = Object.keys(sources);
    for(var k in keys) {
        sources[keys[k]].src = "maps/" + keys[k] + ".jpg";
    }

    $(mapPreviewCanvas).css({"border": "10px solid midnightblue", "display": "none", "position": "absolute"})
        .attr({ "width": 400, "height": 400});
    $("body").append(mapPreviewCanvas);




});