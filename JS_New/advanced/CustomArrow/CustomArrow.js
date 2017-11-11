/*自定义3D箭头标绘
x1,y1,z1是起点
x2,y2,z2是终点
height1是中心点高度         可为null，默认值30
height2是顶面与底面的高度   可为null，默认值10
*/
function CreateCustom3DArrow(x1, y1, z1, x2, y2, z2, height1, height2) {
    //总长度
    var countLength = Math.sqrt((x1-x2)*(x1-x2) + (x1-x2)*(x1-x2) + (x1-x2)*(x1-x2));
    //等分数
    var num = 3;
    //间隔
    var gapValue = countLength / num;
    //抛物线点数组
    var pointArray = new Array(num * 3);
    //箭头地标数组
    var arrowArray = new Array(num * 6 + 6);
    //抛物线方程 y=a*x^2+b*x+c
    var height;

    if (height1 == null) {
        height = 30;
    }
    else {
        height = height1;
    }

    var _y = Math.round(height / gapValue);
    var _b = _y / 5;
    var _a = -(_b * 0.05);
    //箭头数组
    var finalArrowArray = new Array(arrowArray.length * 4);
    //箭头高度
    var arrowHeight;

    if (height2 == null) {
        arrowHeight = 10;
    }
    else {
        arrowHeight = height2;
    }

    //------------------------------------------------------------------------------------------
    //插入起点
    pointArray[0] = x1;
    pointArray[1] = y1;
    pointArray[2] = z1;
    //插入终点
    pointArray[num * 3 - 3] = x2;
    pointArray[num * 3 - 2] = y2;
    pointArray[num * 3 - 1] = z2;
    //当前距离
    var nowLength = gapValue;
    //计算抛物线
    for (var i = 3; i < pointArray.length - 3; i += 3) {
        pointArray[i] = x1 + (x2 - x1) * (nowLength / countLength);
        pointArray[i + 1] = y1 + (y2 - y1) * (nowLength / countLength);
        pointArray[i + 2] = (_a * (i / 3) * (i / 3) + _b * (i / 3)) * gapValue + z1;

        nowLength += gapValue;
    }

    //构建箭头边缘点
    //插入顶点
    arrowArray[0] = pointArray[0];
    arrowArray[1] = pointArray[1];
    arrowArray[2] = pointArray[2];
    //插入终点
    arrowArray[arrowArray.length - 3] = pointArray[0];
    arrowArray[arrowArray.length - 2] = pointArray[1];
    arrowArray[arrowArray.length - 1] = pointArray[2];

    //计算角边 左 x=x2-(y1-y2) y=y2+(x1-x2) 右 x=x2+(y1-y2) y=y2-(x1-x2)
    //计算腰边 左 x=xn-(y1-yn) y=yn+(x1-xn) 右 x=xn+(y1-yn) y=yn-(x1-xn) 注意N为中线上任意一点

    for (var i = 3; i < pointArray.length; i += 3) {
        if (i < 21) {
            //计算顶点x1y1与当前点的差值
            var xValue1 = pointArray[0] - pointArray[i];
            var yValue1 = pointArray[1] - pointArray[i + 1];
            //角边
            arrowArray[i] = pointArray[i] - yValue1;
            arrowArray[i + 1] = pointArray[i + 1] + xValue1;
            arrowArray[i + 2] = pointArray[i + 2];
            //对点
            arrowArray[arrowArray.length - i - 3] = pointArray[i] + yValue1;
            arrowArray[arrowArray.length - i - 2] = pointArray[i + 1] - xValue1;
            arrowArray[arrowArray.length - i - 1] = pointArray[i + 2];

            if (i == 18) {
                //计算顶点x1y1与第3点的差值
                var xValue2 = pointArray[0] - pointArray[6];
                var yValue2 = pointArray[1] - pointArray[7];
                //腰边顶点
                arrowArray[i + 3] = pointArray[i] - yValue2;
                arrowArray[i + 4] = pointArray[i + 1] + xValue2;
                arrowArray[i + 5] = pointArray[i + 2];
                //对点
                arrowArray[arrowArray.length - i - 6] = pointArray[i] + yValue2;
                arrowArray[arrowArray.length - i - 5] = pointArray[i + 1] - xValue2;
                arrowArray[arrowArray.length - i - 4] = pointArray[i + 2];
            }
        }
        else {
            //计算顶点x1y1与第3点的差值
            var xValue3 = pointArray[0] - pointArray[6];
            var yValue3 = pointArray[1] - pointArray[7];
            //腰边
            arrowArray[i + 3] = pointArray[i] - yValue3;
            arrowArray[i + 4] = pointArray[i + 1] + xValue3;
            arrowArray[i + 5] = pointArray[i + 2];
            //对点
            arrowArray[arrowArray.length - i - 6] = pointArray[i] + yValue3;
            arrowArray[arrowArray.length - i - 5] = pointArray[i + 1] - xValue3;
            arrowArray[arrowArray.length - i - 4] = pointArray[i + 2];
        }
    }

    //构建箭头数组

    //顶层
    for (var i = arrowArray.length / 2; i > 0; i -= 3) {
        finalArrowArray[arrowArray.length - i * 2] = arrowArray[i - 3];
        finalArrowArray[arrowArray.length - i * 2 + 1] = arrowArray[i - 2];
        finalArrowArray[arrowArray.length - i * 2 + 2] = arrowArray[i - 1] + arrowHeight;

        finalArrowArray[arrowArray.length - i * 2 + 3] = arrowArray[arrowArray.length - i];
        finalArrowArray[arrowArray.length - i * 2 + 4] = arrowArray[arrowArray.length - i + 1];
        finalArrowArray[arrowArray.length - i * 2 + 5] = arrowArray[arrowArray.length - i + 2] + arrowHeight;
    }

    //中间层
    for (var i = 0; i < arrowArray.length; i += 3) {
        finalArrowArray[i * 2 + arrowArray.length] = arrowArray[i];
        finalArrowArray[i * 2 + 1 + arrowArray.length] = arrowArray[i + 1];
        finalArrowArray[i * 2 + 2 + arrowArray.length] = arrowArray[i + 2];

        finalArrowArray[i * 2 + 3 + arrowArray.length] = arrowArray[i];
        finalArrowArray[i * 2 + 4 + arrowArray.length] = arrowArray[i + 1];
        finalArrowArray[i * 2 + 5 + arrowArray.length] = arrowArray[i + 2] + arrowHeight;
    }

    //底层
    for (var i = 0; i < arrowArray.length / 2; i += 3) {
        finalArrowArray[i * 2 + arrowArray.length * 3] = arrowArray[i];
        finalArrowArray[i * 2 + 1 + arrowArray.length * 3] = arrowArray[i + 1];
        finalArrowArray[i * 2 + 2 + arrowArray.length * 3] = arrowArray[i + 2];

        finalArrowArray[i * 2 + 3 + arrowArray.length * 3] = arrowArray[arrowArray.length - i - 3];
        finalArrowArray[i * 2 + 4 + arrowArray.length * 3] = arrowArray[arrowArray.length - i - 2];
        finalArrowArray[i * 2 + 5 + arrowArray.length * 3] = arrowArray[arrowArray.length - i - 1];
    }

	return finalArrowArray;
}

/*2D曲线箭头标绘
x1,y1  起点
x2,y2  中点 影响曲度
x3,y3  终点
height 离地高度 可为null，默认值10
*/
function CreateCurveArrow2D(x1, y1, x2, y2, x3, y3, height) {
    //离地面高度
    var h;

    if (height == null) {
        h = 10;
    }
    else {
        h = height;
    }
    //计算角边 左 x=x2-(y1-y2) y=y2+(x1-x2) 右 x=x2+(y1-y2) y=y2-(x1-x2)
    //构造左右基础折线点
    var p0x = x1 - (x1 - x2) * 0.01;
    var p0y = y1 - (y1 - y2) * 0.01;
    var p1x = x2 + (x1 - x2) * 0.05;
    var p1y = y2 + (y1 - y2) * 0.05;
    var p2x = x3 + (x2 - x3) * 0.1;
    var p2y = y3 + (y2 - y3) * 0.1;

    var p0lx = x1 + (p0y - y1);
    var p0ly = y1 - (p0x - x1);
    var p0rx = x1 - (p0y - y1);
    var p0ry = y1 + (p0x - x1);
    var p1lx = x2 - (p1y - y2);
    var p1ly = y2 + (p1x - x2);
    var p1rx = x2 + (p1y - y2);
    var p1ry = y2 - (p1x - x2);
    var p2lx = x3 - (p2y - y3);
    var p2ly = y3 + (p2x - x3);
    var p2rx = x3 + (p2y - y3);
    var p2ry = y3 - (p2x - x3);
    //构造三条曲线
    var curveLineM = GetCurveLineArrayBy3Point(x1, y1, x2, y2, x3, y3, h);
    var curveLineL = GetCurveLineArrayBy3Point(p0lx, p0ly, p1lx, p1ly, p2lx, p2ly, h);
    var curveLineR = GetCurveLineArrayBy3Point(p0rx, p0ry, p1rx, p1ry, p2rx, p2ry, h);
    //构造箭头角边点
    var leftPointX = curveLineM[27] - (curveLineM[13] - curveLineM[28]);
    var leftPointY = curveLineM[28] + (curveLineM[12] - curveLineM[27]);
    var rightPointX = curveLineM[27] + (curveLineM[13] - curveLineM[28]);
    var rightPointY = curveLineM[28] - (curveLineM[12] - curveLineM[27]);
    //构造箭头底点
    var bottomPointX = curveLineM[273];
    var bottomPointY = curveLineM[274];
    //构造曲线箭头点数组
    var curveLine = new Array(588);

    for (var i = 0; i < curveLine.length; i += 3) {
        //顶尖
        if (i == 0) {
            curveLine[0] = x1;
            curveLine[1] = y1;
            curveLine[2] = h;
        } //左角点
        else if (i == 3) {
            curveLine[3] = leftPointX;
            curveLine[4] = leftPointY;
            curveLine[5] = h;
        } //左边
        else if (i >= 6 && i <= 291) {
            curveLine[i] = curveLineL[i + 9];
            curveLine[i + 1] = curveLineL[i + 10];
            curveLine[i + 2] = curveLineL[i + 11];
        } //底点
        else if (i == 294) {
            curveLine[i] = bottomPointX;
            curveLine[i + 1] = bottomPointY;
            curveLine[i + 2] = h;
        } //右边
        else if (i >= 297 && i <= 582) {
            curveLine[i] = curveLineR[597 - i];
            curveLine[i + 1] = curveLineR[598 - i];
            curveLine[i + 2] = curveLineR[599 - i];
        } //右角点
        else if (i == 585) {
            curveLine[i] = rightPointX;
            curveLine[i + 1] = rightPointY;
            curveLine[i + 2] = h;
        }
    }
	
	return curveLine;
}

/*2D多箭头标绘(2箭头)
ax1,ay1,ax2,ay2 第1个箭头的首尾点
ax3,ay3,ax4,ay4 第2个箭头的首尾点
ox1,oy1,ox2,oy2 左右偏移点 影响曲度
ox3,oy3         底部偏移点 影响曲度
height          离地高度 可为null，默认值10
*/
function CreateMultiArrow2D_2(ax1, ay1, ax2, ay2, ax3, ay3, ax4, ay4, ox1, oy1, ox2, oy2, ox3, oy3, height) {
    //离地高度
    var h;

    if (height == null) {
        h = 10;
    }
    else {
        h = height;
    }
    //计算顶点偏移点 左 x=x2-(y1-y2) y=y2+(x1-x2) 右 x=x2+(y1-y2) y=y2-(x1-x2)
    var p0xl = ax1 + (ay2 - ay1) * 0.01;
    var p0yl = ay1 - (ax2 - ax1) * 0.01;
    var p0xr = ax1 - (ay2 - ay1) * 0.01;
    var p0yr = ay1 + (ax2 - ax1) * 0.01;
    var p1xl = ax3 + (ay4 - ay3) * 0.01;
    var p1yl = ay3 - (ax4 - ax3) * 0.01;
    var p1xr = ax3 - (ay4 - ay3) * 0.01;
    var p1yr = ay3 + (ax4 - ax3) * 0.01;
    //计算必要点
    var p0x = ax1 - (ax1 - ox1) * 1.5;
    var p0y = ay1 - (ay1 - oy1) * 1.5;
    var p1x = ax3 - (ax3 - ox2) * 1.5;
    var p1y = ay3 - (ay3 - oy2) * 1.5;
    //构造4条主要曲线数组
    var leftLine = GetCurveLineArrayBy3Point(p0xl, p0yl, ox1, oy1, ax2, ay2, h);
    var rightLine = GetCurveLineArrayBy3Point(p1xr, p1yr, ox2, oy2, ax4, ay4, h);
    var bottomLine = GetCurveLineArrayBy3Point(ax2, ay2, ox3, oy3, ax4, ay4, h);
    var a0Line = GetCurveLineArrayBy4Point(p0xr, p0yr, p0x, p0y, p1x, p1y, p1xl, p1yl, h);
    //构造箭头角
    var firstx = ax1 - (ax1 - ox1) * 0.1;
    var firsty = ay1 - (ay1 - oy1) * 0.1;
    var secondx = ax1 - (ax1 - ox1) * 0.2;
    var secondy = ay1 - (ay1 - oy1) * 0.2;

    var angle1xl = secondx - (firsty - secondy);
    var angle1yl = secondy + (firstx - secondx);
    var angle1xr = secondx + (firsty - secondy);
    var angle1yr = secondy - (firstx - secondx);

    firstx = ax3 - (ax3 - ox2) * 0.1;
    firsty = ay3 - (ay3 - oy2) * 0.1;
    secondx = ax3 - (ax3 - ox2) * 0.2;
    secondy = ay3 - (ay3 - oy2) * 0.2;

    var angle2xl = secondx - (firsty - secondy);
    var angle2yl = secondy + (firstx - secondx);
    var angle2xr = secondx + (firsty - secondy);
    var angle2yr = secondy - (firstx - secondx);
    //构造多箭头标绘
    var multiArrow = new Array(1230);

    for (var i = 0; i < multiArrow.length; i += 3) {
        if (i == 0) {
            multiArrow[i] = angle1xl;
            multiArrow[i + 1] = angle1yl;
            multiArrow[i + 2] = h;
        }
        else if (i == 3) {
            multiArrow[i] = ox1 + (ax1 - ox1) * 1.2;
            multiArrow[i + 1] = oy1 + (ay1 - oy1) * 1.2;
            multiArrow[i + 2] = h;
        }
        else if (i == 6) {
            multiArrow[i] = angle1xr;
            multiArrow[i + 1] = angle1yr;
            multiArrow[i + 2] = h;
        }
        else if (i >= 9 && i < 312) {
            multiArrow[i] = a0Line[i - 9];
            multiArrow[i + 1] = a0Line[i - 8];
            multiArrow[i + 2] = h;
        }
        else if (i == 312) {
            multiArrow[i] = angle2xl;
            multiArrow[i + 1] = angle2yl;
            multiArrow[i + 2] = h;
        }
        else if (i == 315) {
            multiArrow[i] = ox2 + (ax3 - ox2) * 1.2;
            multiArrow[i + 1] = oy2 + (ay3 - oy2) * 1.2;
            multiArrow[i + 2] = h;
        }
        else if (i == 318) {
            multiArrow[i] = angle2xr;
            multiArrow[i + 1] = angle2yr;
            multiArrow[i + 2] = h;
        }
        else if (i >= 321 && i < 624) {
            multiArrow[i] = rightLine[i - 321];
            multiArrow[i + 1] = rightLine[i - 320];
            multiArrow[i + 2] = h;
        }
        else if (i >= 624 && i < 927) {
            multiArrow[i] = bottomLine[924 - i];
            multiArrow[i + 1] = bottomLine[925 - i];
            multiArrow[i + 2] = h;
        }
        else if (i >= 927 && i < 1230) {
            multiArrow[i] = leftLine[1227 - i];
            multiArrow[i + 1] = leftLine[1228 - i];
            multiArrow[i + 2] = h;
        }
    }
	
	return multiArrow;
}

/*2D多箭头标绘(3箭头)
ax1,ay1,ax2,ay2 第1个箭头的首尾点
ax3,ay3,ax4,ay4 第2个箭头的首尾点
ax5,ay5,ax6,ay6 第3个箭头的首尾点
ox1,oy1,ox2,oy2 左右偏移点 影响曲度
height          离地高度 可为null，默认值10
*/
function CreateMultiArrow2D_3(ax1, ay1, ax2, ay2, ax3, ay3, ax4, ay4, ax5, ay5, ax6, ay6, ox1, oy1, ox2, oy2, height) {
    //离地高度
    var h;

    if (height == null) {
        h = 10;
    }
    else {
        h = height;
    }
    //计算顶点偏移点 左 x=x2-(y1-y2) y=y2+(x1-x2) 右 x=x2+(y1-y2) y=y2-(x1-x2)
    var p0xl = ax1 + (ay2 - ay1) * 0.01;
    var p0yl = ay1 - (ax2 - ax1) * 0.01;
    var p0xr = ax1 - (ay2 - ay1) * 0.01;
    var p0yr = ay1 + (ax2 - ax1) * 0.01;
    var p1xl = ax3 + (ay4 - ay3) * 0.01;
    var p1yl = ay3 - (ax4 - ax3) * 0.01;
    var p1xr = ax3 - (ay4 - ay3) * 0.01;
    var p1yr = ay3 + (ax4 - ax3) * 0.01;
    var p2xl = ax5 + (ay6 - ay5) * 0.01;
    var p2yl = ay5 - (ax6 - ax5) * 0.01;
    var p2xr = ax5 - (ay6 - ay5) * 0.01;
    var p2yr = ay5 + (ax6 - ax5) * 0.01;
    //计算必要点
    var p0x = ax1 - (ax1 - ox1) * 1.5;
    var p0y = ay1 - (ay1 - oy1) * 1.5;
    var p1x = ax3 - (ax3 - ax4) * 0.9;
    var p1y = ay3 - (ay3 - ay4) * 0.9;
    var p2x = ax5 - (ax5 - ox2) * 1.5;
    var p2y = ay5 - (ay5 - oy2) * 1.5;
    //构造5条主要曲线数组
    var leftLine = GetCurveLineArrayBy3Point(p0xl, p0yl, ox1, oy1, ax2, ay2, h);
    var rightLine = GetCurveLineArrayBy3Point(p2xr, p2yr, ox2, oy2, ax6, ay6, h);
    var bottomLine = GetCurveLineArrayBy3Point(ax2, ay2, ax4, ay4, ax6, ay6, h);
    var a0Line = GetCurveLineArrayBy4Point(p0xr, p0yr, p0x, p0y, p1x, p1y, p1xl, p1yl, h);
    var a1Line = GetCurveLineArrayBy4Point(p1xr, p1yr, p1x, p1y, p2x, p2y, p2xl, p2yl, h);
    //构造箭头角
    var firstx = ax1 - (ax1 - ox1) * 0.1;
    var firsty = ay1 - (ay1 - oy1) * 0.1;
    var secondx = ax1 - (ax1 - ox1) * 0.2;
    var secondy = ay1 - (ay1 - oy1) * 0.2;

    var angle1xl = secondx - (firsty - secondy);
    var angle1yl = secondy + (firstx - secondx);
    var angle1xr = secondx + (firsty - secondy);
    var angle1yr = secondy - (firstx - secondx);

    firstx = ax3 - (ax3 - ax4) * 0.05;
    firsty = ay3 - (ay3 - ay4) * 0.05;
    secondx = ax3 - (ax3 - ax4) * 0.1;
    secondy = ay3 - (ay3 - ay4) * 0.1;

    var angle2xl = secondx - (firsty - secondy);
    var angle2yl = secondy + (firstx - secondx);
    var angle2xr = secondx + (firsty - secondy);
    var angle2yr = secondy - (firstx - secondx);

    firstx = ax5 - (ax5 - ox2) * 0.1;
    firsty = ay5 - (ay5 - oy2) * 0.1;
    secondx = ax5 - (ax5 - ox2) * 0.2;
    secondy = ay5 - (ay5 - oy2) * 0.2;

    var angle3xl = secondx - (firsty - secondy);
    var angle3yl = secondy + (firstx - secondx);
    var angle3xr = secondx + (firsty - secondy);
    var angle3yr = secondy - (firstx - secondx);
    //构造多箭头标绘
    var multiArrow = new Array(1542);

    for (var i = 0; i < multiArrow.length; i += 3) {
        if (i == 0) {
            multiArrow[i] = angle1xl;
            multiArrow[i + 1] = angle1yl;
            multiArrow[i + 2] = h;
        }
        else if (i == 3) {
            multiArrow[i] = ox1 + (ax1 - ox1) * 1.2;
            multiArrow[i + 1] = oy1 + (ay1 - oy1) * 1.2;
            multiArrow[i + 2] = h;
        }
        else if (i == 6) {
            multiArrow[i] = angle1xr;
            multiArrow[i + 1] = angle1yr;
            multiArrow[i + 2] = h;
        }
        else if (i >= 9 && i < 312) {
            multiArrow[i] = a0Line[i - 9];
            multiArrow[i + 1] = a0Line[i - 8];
            multiArrow[i + 2] = h;
        }
        else if (i == 312) {
            multiArrow[i] = angle2xl;
            multiArrow[i + 1] = angle2yl;
            multiArrow[i + 2] = h;
        }
        else if (i == 315) {
            multiArrow[i] = ax4 + (ax3 - ax4) * 1.1;
            multiArrow[i + 1] = ay4 + (ay3 - ay4) * 1.1;
            multiArrow[i + 2] = h;
        }
        else if (i == 318) {
            multiArrow[i] = angle2xr;
            multiArrow[i + 1] = angle2yr;
            multiArrow[i + 2] = h;
        }
        else if (i >= 321 && i < 624) {
            multiArrow[i] = a1Line[i - 321];
            multiArrow[i + 1] = a1Line[i - 320];
            multiArrow[i + 2] = h;
        }
        else if (i == 624) {
            multiArrow[i] = angle3xl;
            multiArrow[i + 1] = angle3yl;
            multiArrow[i + 2] = h;
        }
        else if (i == 627) {
            multiArrow[i] = ox2 + (ax5 - ox2) * 1.2;
            multiArrow[i + 1] = oy2 + (ay5 - oy2) * 1.2;
            multiArrow[i + 2] = h;
        }
        else if (i == 630) {
            multiArrow[i] = angle3xr;
            multiArrow[i + 1] = angle3yr;
            multiArrow[i + 2] = h;
        }
        else if (i >= 633 && i < 936) {
            multiArrow[i] = rightLine[i - 633];
            multiArrow[i + 1] = rightLine[i - 632];
            multiArrow[i + 2] = h;
        }
        else if (i >= 936 && i < 1239) {
            multiArrow[i] = bottomLine[1236 - i];
            multiArrow[i + 1] = bottomLine[1237 - i];
            multiArrow[i + 2] = h;
        }
        else if (i >= 1239 && i < 1542) {
            multiArrow[i] = leftLine[1539 - i];
            multiArrow[i + 1] = leftLine[1540 - i];
            multiArrow[i + 2] = h;
        }
    }
	
	return multiArrow;
}

//根据3个点获取曲线数组
function GetCurveLineArrayBy3Point(x1, y1, x2, y2, x3, y3, h) {
    //曲线点数组
    var curveLine = new Array(303);
    //系数
    var t = 0;
    //贝塞尔曲线(3点) (1 - t)^2 * p0 + 2 * (1 - t) * t * p1 + t^2 * p2
    for (var i = 0; i <= 100; i++) {
        var _x = Math.pow((1 - t), 2) * x1 + 2 * (1 - t) * t * x2 + Math.pow(t, 2) * x3;
        var _y = Math.pow((1 - t), 2) * y1 + 2 * (1 - t) * t * y2 + Math.pow(t, 2) * y3;

        curveLine[i * 3] = _x;
        curveLine[i * 3 + 1] = _y;
        curveLine[i * 3 + 2] = h;

        t += 0.01;
    }

    return curveLine;
}
//根据4个点获取曲线数组
function GetCurveLineArrayBy4Point(x1, y1, x2, y2, x3, y3, x4, y4, h) {
    //曲线点数组
    var curveLine = new Array(303);
    //系数
    var t = 0;
    //贝塞尔曲线(4点) (1 - t)^3 * p0 + 3 * (1 - t)^2 * t * p1 + 3 * (1 - t) * t^2 * p2 + t^3 * p3
    for (var i = 0; i <= 100; i++) {
        var _x = Math.pow((1 - t), 3) * x1 + 3 * Math.pow((1 - t), 2) * t * x2 + 3 * (1 - t) * Math.pow(t, 2) * x3 + Math.pow(t, 3) * x4;
        var _y = Math.pow((1 - t), 3) * y1 + 3 * Math.pow((1 - t), 2) * t * y2 + 3 * (1 - t) * Math.pow(t, 2) * y3 + Math.pow(t, 3) * y4;

        curveLine[i * 3] = _x;
        curveLine[i * 3 + 1] = _y;
        curveLine[i * 3 + 2] = h;

        t += 0.01;
    }

    return curveLine;
}