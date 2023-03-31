using rakekiyo.GoGame;

var regulations = new Regulations(boardSize: 9, komi: 6.5);
var taikyoku = new Taikyoku(regulations);

taikyoku.start();

