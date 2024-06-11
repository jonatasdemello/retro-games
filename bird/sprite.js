function Shape(x, y, largura, altura) {
	this.x = x;
	this.y = y;
	this.largura = largura;
	this.altura = altura;
	
	this.desenha = function(xCanvas, yCanvas) {
		ctx.drawImage(img, this.x, this.y, this.largura, this.altura, xCanvas, yCanvas, this.largura, this.altura);
	}
	this.recorta = function(xCanvas, yCanvas, recoPx, recoPy, recoWd, recoHt) {
		ctx.drawImage(img, this.x + recoPx, this.y + recoPy, recoWd, recoHt, xCanvas, yCanvas, recoWd, recoHt);
	}
}

function Sprite(x, y, largura, altura) {
	this.x = x;
	this.y = y;
	this.largura = largura;
	this.altura = altura;

	this.desenha = function(xCanvas, yCanvas, posx, posy) {
		ctx.drawImage(img, this.x + (largura * posx), this.y + (altura * posy),
		                   this.largura, this.altura, xCanvas, yCanvas, this.largura, this.altura);
	}
}

function SptRsz(x, y, largura, altura) {
	this.x = x;
	this.y = y;
	this.largura = largura;
	this.altura = altura;

	this.desenha = function(xCanvas, yCanvas, posx, posy, tmh) {
	    if ((tmh < 0) || (tmh == '')) { tmh = 1; }
		ctx.drawImage(img, this.x + (largura * posx), this.y + (altura * posy),
		                   this.largura, this.altura, xCanvas, yCanvas, (this.largura * tmh), (this.altura * tmh));
	}
}

