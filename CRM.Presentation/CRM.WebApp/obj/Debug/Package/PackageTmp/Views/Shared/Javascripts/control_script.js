// JavaScript Document
function MM_jumpMenu(targ,selObj,restore){ //v3.0
  eval(targ+".location='"+selObj.options[selObj.selectedIndex].value+"'");
  if (restore) selObj.selectedIndex=0;
}


/***********************************************
* Pop-it menu- © Dynamic Drive (www.dynamicdrive.com)
* This notice MUST stay intact for legal use
* Visit http://www.dynamicdrive.com/ for full source code
***********************************************/

var defaultMenuWidth="150px" //set default menu width.

var linkset=new Array()
//SPECIFY MENU SETS AND THEIR LINKS. FOLLOW SYNTAX LAID OUT

linkset[0]='<a href="http://www.dadabhagwan.org/enlightenment.html">Enlightenment</a>'
linkset[0]+='<a href="http://www.dadabhagwan.org/liberation.html">Liberation</a>'
linkset[0]+='<a href="http://www.dadabhagwan.org/spirituality.html">Spirituality</a>'
linkset[0]+='<a href="http://www.dadabhagwan.org/religion.html">True Religion</a>'
linkset[0]+='<a href="http://www.dadabhagwan.org/eternal-happiness.html">Eternal Happiness</a>'
linkset[0]+='<a href="http://www.dadabhagwan.org/inner-peace.html">Inner Peace </a>'
linkset[0]+='<a href="http://www.dadabhagwan.org/bliss.html">Eternal Bliss </a>'
linkset[0]+='<a href="http://www.dadabhagwan.org/salvation.html">Salvation</a>'

linkset[1]='<a href="http://www.dadabhagwan.org/bhagwan-realization.html">Who Is Dada Bhagwan</a>'
linkset[1]+='<a href="http://www.dadabhagwan.org/niruma/self-knowledge.html">Pujya Niruma</a>'
linkset[1]+='<a href="http://www.dadabhagwan.org/spiritual-celibacy.html">Pujya Deepakbhai</a>'

////No need to edit beyond here

var ie5=document.all && !window.opera
var ns6=document.getElementById

if (ie5||ns6)
document.write('<div id="popitmenu" onmouseover="clearhidemenu();" onmouseout="dynamichide(event)"></div>')

function iecompattest(){
return (document.compatMode && document.compatMode.indexOf("CSS")!=-1)? document.documentElement : document.body
}

function showmenu(e, which, optWidth){
if (!document.all&&!document.getElementById)
return
clearhidemenu()
menuobj=ie5? document.all.popitmenu : document.getElementById("popitmenu")
menuobj.innerHTML=which
menuobj.style.width=(typeof optWidth!="undefined")? optWidth : defaultMenuWidth
menuobj.contentwidth=menuobj.offsetWidth
menuobj.contentheight=menuobj.offsetHeight
eventX=ie5? event.clientX : e.clientX
eventY=ie5? event.clientY : e.clientY
//Find out how close the mouse is to the corner of the window
var rightedge=ie5? iecompattest().clientWidth-eventX : window.innerWidth-eventX
var bottomedge=ie5? iecompattest().clientHeight-eventY : window.innerHeight-eventY
//if the horizontal distance isn't enough to accomodate the width of the context menu
if (rightedge<menuobj.contentwidth)
//move the horizontal position of the menu to the left by it's width
menuobj.style.left=ie5? iecompattest().scrollLeft+eventX-menuobj.contentwidth+"px" : window.pageXOffset+eventX-menuobj.contentwidth+"px"
else
//position the horizontal position of the menu where the mouse was clicked
menuobj.style.left=ie5? iecompattest().scrollLeft+eventX+"px" : window.pageXOffset+eventX+"px"
//same concept with the vertical position
if (bottomedge<menuobj.contentheight)
menuobj.style.top=ie5? iecompattest().scrollTop+eventY-menuobj.contentheight+"px" : window.pageYOffset+eventY-menuobj.contentheight+"px"
else
menuobj.style.top=ie5? iecompattest().scrollTop+event.clientY+"px" : window.pageYOffset+eventY+"px"
menuobj.style.visibility="visible"
return false
}

function contains_ns6(a, b) {
//Determines if 1 element in contained in another- by Brainjar.com
while (b.parentNode)
if ((b = b.parentNode) == a)
return true;
return false;
}

function hidemenu(){
if (window.menuobj)
menuobj.style.visibility="hidden"
}

function dynamichide(e){
if (ie5&&!menuobj.contains(e.toElement))
hidemenu()
else if (ns6&&e.currentTarget!= e.relatedTarget&& !contains_ns6(e.currentTarget, e.relatedTarget))
hidemenu()
}

function delayhidemenu(){
delayhide=setTimeout("hidemenu()",500)
}

function clearhidemenu(){
if (window.delayhide)
clearTimeout(delayhide)
}


if (ie5||ns6)
document.onclick=hidemenu