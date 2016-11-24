var canvas = document.getElementById('canvas'),
    context = canvas.getContext('2d'),
    image = new Image(),
    gradient = context.createLinearGradient(0, 0,
                  canvas.width, canvas.height),
    text1 = 'Welcome to the',
    text2 = '      Pet store!',
    pattern; // create pattern after image loads

function drawBackground() {
    var STEP_Y = 12,
        TOP_MARGIN = STEP_Y * 4,
        LEFT_MARGIN = 35,
        i = context.canvas.height;

    context.save();

    context.strokeStyle = 'lightgray';
    context.lineWidth = 0.5;

    while (i > TOP_MARGIN) { // Draw horizontal lines from bottom up
        context.beginPath();
        context.moveTo(0, i);
        context.lineTo(context.canvas.width, i);
        context.stroke();
        i -= STEP_Y;
    }

    // Draw vertical line
    context.strokeStyle = 'rgba(100,0,0,0.3)';
    context.lineWidth = 1;

    context.beginPath();
    context.moveTo(LEFT_MARGIN, 0);
    context.lineTo(LEFT_MARGIN, context.canvas.height);
    context.stroke();

    context.restore();
}

function drawGradientText1() {
    context.fillStyle = gradient;
    context.fillText(text1, 65, 200);
    context.strokeText(text1, 65, 200);
}

function drawGradientText2() {
    context.fillStyle = pattern;
    context.fillText(text2, 65, 450);
    context.strokeText(text2, 65, 450);
}

// Initialization

context.font = '100px Palatino';
context.strokeStyle = 'cornflowerblue';

if (navigator.userAgent.indexOf('Opera') === -1)
    context.shadowColor = 'rgba(100, 100, 150, 0.8)';

context.shadowOffsetX = 5;
context.shadowOffsetY = 5;
context.shadowBlur = 10;

gradient.addColorStop(0, 'Green');
gradient.addColorStop(0.25, 'Green');
gradient.addColorStop(0.5, 'white');
gradient.addColorStop(0.75, 'blue');
gradient.addColorStop(1.0, 'blue');

drawGradientText1();
drawGradientText2();
