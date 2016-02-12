
var prevIndex=-1;
function TabClick(tabId,Index) {	
	if(prevIndex >-1 ){		
		var pTab = el(tabId + prevIndex +"Tab")
		var pCon = el(tabId + prevIndex +"Content")
		pTab.className = "tab";				
		pCon.style.display  = "none";	
	}	
	var cTab = el(tabId + Index +"Tab")
	var cCon = el(tabId + Index +"Content")
	
	cTab.className = "seltab";				
	cCon.style.display  = "block";	
	prevIndex = Index;	
}
function el(sId){
	if(document.layers) {
		return document.layers[sId];
	} if (document.all) {
	   return document.all(sId);
	} else {
	   return document.getElementById(sId);
	}
}

function JSTabs(width,height) {
	this.width = (!isNaN(width))?width:"";
	this.height = (!isNaN(height))?height:"";
	this.spacing = 1;	
	this.defaultTab = 0;
	this.id = "JSTabs1";
	this.tabWidth = Math.round( (this.width/this.count) - ( this.spacing * (this.count-1) )  );
	
	this.tabs = new Array();
	this.addTab = _addTab;
	this.build = _build;
	return this;
}

function tab(caption,content,content_type){
	this.caption = caption;
	this.content = content;
	this.content_type = content_type;
	return this;
}

function _addTab() {
	this.count = this.tabs.length + 1;
	this.tabs[this.tabs.length] = new tab();
	this.tabs[this.tabs.length-1].caption = arguments[0];
	var s;
	switch (arguments[2]){
		case 1: s = arguments[1];
			break;
		case 2: s = "<iframe src=\""+ arguments[1] +"\" width=\""+ (this.width-10) /*+"\" height=\""+ (this.height-10)*/ +"\"></iframe>";
			break;
		case 3: s = "<img src=\""+ arguments[1] +"\" >";
			break;	  
	   default: s = arguments[1];			
	}
	this.tabs[this.tabs.length-1].content = s;
}

function _build(){
	var s,s2="";
	s =""
	s += "<table width=\""+ this.width +"\" border=0 cellspacing=\"0\" cellpaddin=\"0\" ><tr>";	
	for(var i=0;i<this.count;i++){
		s += "<td id=\""+ this.id + i +"Tab\" class=\"tab\" onclick=\"TabClick('"+ this.id +"',"+ i +")\" width=\""+ this.tabWidth +"\" >"+ this.tabs[i].caption +"</td>";
		s2 += "<div id=\""+ this.id + i +"Content\" class=\"tabContent\" style=\"height:"+this.height+"px;\">"+ this.tabs[i].content +"</div>";
		if (i<this.count-1 && this.spacing>0 ) {
			s += "<td class=\"tabSpacer\" width=\""+ this.spacing +"\">&nbsp;</td>";
		}
	}	
	s += "</tr><tr><td valign=\"top\" colspan=\""+ ((this.count * 2)-1) +"\" width=\""+ this.width +"\" class=\"cell\" height=\""+ this.height +"\">"
	s += s2 + "</td></tr></table>";
	document.write (s);
	TabClick(this.id,this.defaultTab);
}

