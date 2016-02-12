// JavaScript Document
var random_num = Math.random() ;
var picnum = Math.round(random_num*28);

function banner_rotate()
{
	//alert(picnum);
	var images = new Array(29);							
		images[0] = "<table width='100%' height='253' border='0' align='left' cellpadding='0' cellspacing='0' style='background-color:#192114;'><tr height='253'><td  style='background-color:#192114;' bgcolor='#192114'><img src='http://www.dadabhagwan.org/images/master_banner.jpg' height='253' width='779' /></td></tr></table>";							
		images[1] = "<table width='100%' height='253' border='0' align='left' cellpadding='0' cellspacing='0' style='background-color:#DF9E41;'><tr height='253'><td  style='background-color:#DF9E41;' bgcolor='#DF9E41'><img src='http://www.dadabhagwan.org/images/banner/master_banner001.jpg' height='253' width='779' /></td></tr></table>";
		images[2] = "<table width='100%' height='253' border='0' align='left' cellpadding='0' cellspacing='0' style='background-color:#551303;'><tr height='253'><td  style='background-color:#551303;' bgcolor='#551303'><img src='http://www.dadabhagwan.org/images/banner/master_banner002.jpg' height='253' width='779' /></td></tr></table>";							
		images[3] = "<table width='100%' height='253' border='0' align='left' cellpadding='0' cellspacing='0' style='background-color:#316C8C;'><tr height='253'><td  style='background-color:#316C8C;' bgcolor='#316C8C'><img src='http://www.dadabhagwan.org/images/banner/master_banner003.jpg' height='253' width='779' /></td></tr></table>";							
		images[4] = "<table width='100%' height='253' border='0' align='left' cellpadding='0' cellspacing='0' style='background-color:#000000;'><tr height='253'><td  style='background-color:#000000;' bgcolor='#000000'><img src='http://www.dadabhagwan.org/images/banner/master_banner004.jpg' height='253' width='779' /></td></tr></table>";							
		images[5] = "<table width='100%' height='253' border='0' align='left' cellpadding='0' cellspacing='0' style='background-color:#9A5819;'><tr height='253'><td  style='background-color:#9A5819;' bgcolor='#9A5819'><img src='http://www.dadabhagwan.org/images/banner/master_banner005.jpg' height='253' width='779' /></td></tr></table>";							
		images[6] = "<table width='100%' height='253' border='0' align='left' cellpadding='0' cellspacing='0' style='background-color:#802C00;'><tr height='253'><td  style='background-color:#802C00;' bgcolor='#802C00'><img src='http://www.dadabhagwan.org/images/banner/master_banner006.jpg' height='253' width='779' /></td></tr></table>";							
		images[7] = "<table width='100%' height='253' border='0' align='left' cellpadding='0' cellspacing='0' style='background-color:#DE881B;'><tr height='253'><td  style='background-color:#DE881B;' bgcolor='#DE881B'><img src='http://www.dadabhagwan.org/images/banner/master_banner007.jpg' height='253' width='779' /></td></tr></table>";							
		images[8] = "<table width='100%' height='253' border='0' align='left' cellpadding='0' cellspacing='0' style='background-color:#310B00;'><tr height='253'><td  style='background-color:#310B00;' bgcolor='#310B00'><img src='http://www.dadabhagwan.org/images/banner/master_banner008.jpg' height='253' width='779' /></td></tr></table>";							
		images[9] = "<table width='100%' height='253' border='0' align='left' cellpadding='0' cellspacing='0' style='background-color:#470C04;'><tr height='253'><td  style='background-color:#470C04;' bgcolor='#470C04'><img src='http://www.dadabhagwan.org/images/banner/master_banner009.jpg' height='253' width='779' /></td></tr></table>";	
		images[10] = "<table width='100%' height='253' border='0' align='left' cellpadding='0' cellspacing='0' style='background-color:#253046;'><tr height='253'><td  style='background-color:#253046;' bgcolor='#253046'><img src='http://www.dadabhagwan.org/images/banner/master_banner010.jpg' height='253' width='779' /></td></tr></table>";
		images[11] = "<table width='100%' height='253' border='0' align='left' cellpadding='0' cellspacing='0' style='background-color:#512D1D;'><tr height='253'><td  style='background-color:#512D1D;' bgcolor='#512D1D'><img src='http://www.dadabhagwan.org/images/banner/master_banner011.jpg' height='253' width='779' /></td></tr></table>";
		images[12] = "<table width='100%' height='253' border='0' align='left' cellpadding='0' cellspacing='0' style='background-color:#583C8E;'><tr height='253'><td  style='background-color:#583C8E;' bgcolor='#583C8E'><img src='http://www.dadabhagwan.org/images/banner/master_banner012.jpg' height='253' width='779' /></td></tr></table>";
		images[13] = "<table width='100%' height='253' border='0' align='left' cellpadding='0' cellspacing='0' style='background-color:#A6CD1C;'><tr height='253'><td  style='background-color:#A6CD1C;' bgcolor='#A6CD1C'><img src='http://www.dadabhagwan.org/images/banner/master_banner013.jpg' height='253' width='779' /></td></tr></table>";
		images[14] = "<table width='100%' height='253' border='0' align='left' cellpadding='0' cellspacing='0' style='background-color:#DDD5CA;'><tr height='253'><td  style='background-color:#DDD5CA;' bgcolor='#DDD5CA'><img src='http://www.dadabhagwan.org/images/banner/master_banner014.jpg' height='253' width='779' /></td></tr></table>";
		images[15] = "<table width='100%' height='253' border='0' align='left' cellpadding='0' cellspacing='0' style='background-color:#A97306;'><tr height='253'><td  style='background-color:#A97306;' bgcolor='#A97306'><img src='http://www.dadabhagwan.org/images/banner/master_banner015.jpg' height='253' width='779' /></td></tr></table>";
		images[16] = "<table width='100%' height='253' border='0' align='left' cellpadding='0' cellspacing='0' style='background-color:#93D3EE;'><tr height='253'><td  style='background-color:#93D3EE;' bgcolor='#93D3EE'><img src='http://www.dadabhagwan.org/images/banner/master_banner016.jpg' height='253' width='779' /></td></tr></table>";
		images[17] = "<table width='100%' height='253' border='0' align='left' cellpadding='0' cellspacing='0' style='background-color:#2A4D83;'><tr height='253'><td  style='background-color:#2A4D83;' bgcolor='#2A4D83'><img src='http://www.dadabhagwan.org/images/banner/master_banner017.jpg' height='253' width='779' /></td></tr></table>";
		images[18] = "<table width='100%' height='253' border='0' align='left' cellpadding='0' cellspacing='0' style='background-color:#712E04;'><tr height='253'><td  style='background-color:#712E04;' bgcolor='#712E04'><img src='http://www.dadabhagwan.org/images/banner/master_banner018.jpg' height='253' width='779' /></td></tr></table>";	
		images[19] = "<table width='100%' height='253' border='0' align='left' cellpadding='0' cellspacing='0' style='background-color:#986633; ><tr height='253'><td  style='background-color:#986633;'bgcolor='#986633'><img src='http://www.dadabhagwan.org/images/banner/master_banner019.jpg' height='253' width='779' /></td></tr></table>";
		images[20] = "<table width='100%' height='253' border='0' align='left' cellpadding='0' cellspacing='0' style='background-color:#4C4C4C; ><tr height='253'><td  style='background-color:#4C4C4C;'bgcolor='#4C4C4C'><img src='http://www.dadabhagwan.org/images/banner/master_banner020.jpg' height='253' width='779' /></td></tr></table>";
		images[21] = "<table width='100%' height='253' border='0' align='left' cellpadding='0' cellspacing='0' style='background-color:#FDFDFD; ><tr height='253'><td  style='background-color:#FDFDFD;'bgcolor='#FDFDFD'><img src='http://www.dadabhagwan.org/images/banner/master_banner021.jpg' height='253' width='779' /></td></tr></table>";
		images[22] = "<table width='100%' height='253' border='0' align='left' cellpadding='0' cellspacing='0' style='background-color:#302826; ><tr height='253'><td  style='background-color:#302826;'bgcolor='#302826'><img src='http://www.dadabhagwan.org/images/banner/master_banner022.jpg' height='253' width='779' /></td></tr></table>";
		images[23] = "<table width='100%' height='253' border='0' align='left' cellpadding='0' cellspacing='0' style='background-color:#8BC6E8; ><tr height='253'><td  style='background-color:#8BC6E8;'bgcolor='#8BC6E8'><img src='http://www.dadabhagwan.org/images/banner/master_banner023.jpg' height='253' width='779' /></td></tr></table>";
		images[24] = "<table width='100%' height='253' border='0' align='left' cellpadding='0' cellspacing='0' style='background-color:#FFEEBA; ><tr height='253'><td  style='background-color:#FFEEBA;'bgcolor='#FFEEBA'><img src='http://www.dadabhagwan.org/images/banner/master_banner024.jpg' height='253' width='779' /></td></tr></table>";
		images[25] = "<table width='100%' height='253' border='0' align='left' cellpadding='0' cellspacing='0' style='background-color:#232317; ><tr height='253'><td  style='background-color:#232317;'bgcolor='#232317'><img src='http://www.dadabhagwan.org/images/banner/master_banner025.jpg' height='253' width='779' /></td></tr></table>";
		images[26] = "<table width='100%' height='253' border='0' align='left' cellpadding='0' cellspacing='0' style='background-color:#1B3931; ><tr height='253'><td  style='background-color:#1B3931;'bgcolor='#1B3931'><img src='http://www.dadabhagwan.org/images/banner/master_banner026.jpg' height='253' width='779' /></td></tr></table>";
		images[27] = "<table width='100%' height='253' border='0' align='left' cellpadding='0' cellspacing='0' style='background-color:#7F592C; ><tr height='253'><td  style='background-color:#7F592C;'bgcolor='#7F592C'><img src='http://www.dadabhagwan.org/images/banner/master_banner027.jpg' height='253' width='779' /></td></tr></table>";
		images[28] = "<table width='100%' height='253' border='0' align='left' cellpadding='0' cellspacing='0' style='background-color:#1D3A59; ><tr height='253'><td  style='background-color:#1D3A59;'bgcolor='#1D3A59'><img src='http://www.dadabhagwan.org/images/banner/master_banner028.jpg' height='253' width='779' /></td></tr></table>";
		
		var leftimages = new Array(29);
		leftimages[0] = "<img src='http://www.dadabhagwan.org/images/master_banner_left.jpg' width='14' height='253' />";							
		leftimages[1] = "<img src='http://www.dadabhagwan.org/images/banner/master_banner_left001.jpg' width='14' height='253' />";							
		leftimages[2] = "<img src='http://www.dadabhagwan.org/images/banner/master_banner_left002.jpg' width='14' height='253' />";
		leftimages[3] = "<img src='http://www.dadabhagwan.org/images/banner/master_banner_left003.jpg' width='14' height='253' />";							
		leftimages[4] = "<img src='http://www.dadabhagwan.org/images/banner/master_banner_left004.jpg' width='14' height='253' />";							
		leftimages[5] = "<img src='http://www.dadabhagwan.org/images/banner/master_banner_left005.jpg' width='14' height='253' />";							
		leftimages[6] = "<img src='http://www.dadabhagwan.org/images/banner/master_banner_left006.jpg' width='14' height='253' />";							
		leftimages[7] = "<img src='http://www.dadabhagwan.org/images/banner/master_banner_left007.jpg' width='14' height='253' />";							
		leftimages[8] = "<img src='http://www.dadabhagwan.org/images/banner/master_banner_left008.jpg' width='14' height='253' />";							
		leftimages[9] = "<img src='http://www.dadabhagwan.org/images/banner/master_banner_left009.jpg' width='14' height='253' />";							
		leftimages[10] = "<img src='http://www.dadabhagwan.org/images/banner/master_banner_left010.jpg' width='14' height='253' />";
		leftimages[11] = "<img src='http://www.dadabhagwan.org/images/banner/master_banner_left011.jpg' width='14' height='253' />";
		leftimages[12] = "<img src='http://www.dadabhagwan.org/images/banner/master_banner_left012.jpg' width='14' height='253' />";
		leftimages[13] = "<img src='http://www.dadabhagwan.org/images/banner/master_banner_left013.jpg' width='14' height='253' />";	
		leftimages[14] = "<img src='http://www.dadabhagwan.org/images/banner/master_banner_left014.jpg' width='14' height='253' />";
		leftimages[15] = "<img src='http://www.dadabhagwan.org/images/banner/master_banner_left015.jpg' width='14' height='253' />";
		leftimages[16] = "<img src='http://www.dadabhagwan.org/images/banner/master_banner_left016.jpg' width='14' height='253' />";
		leftimages[17] = "<img src='http://www.dadabhagwan.org/images/banner/master_banner_left017.jpg' width='14' height='253' />";
		leftimages[18] = "<img src='http://www.dadabhagwan.org/images/banner/master_banner_left018.jpg' width='14' height='253' />";
		leftimages[19] = "<img src='http://www.dadabhagwan.org/images/banner/master_banner_left019.jpg' width='14' height='253' />";
		leftimages[20] = "<img src='http://www.dadabhagwan.org/images/banner/master_banner_left020.jpg' width='14' height='253' />";
		leftimages[21] = "<img src='http://www.dadabhagwan.org/images/banner/master_banner_left021.jpg' width='14' height='253' />";
		leftimages[22] = "<img src='http://www.dadabhagwan.org/images/banner/master_banner_left022.jpg' width='14' height='253' />";
		leftimages[23] = "<img src='http://www.dadabhagwan.org/images/banner/master_banner_left023.jpg' width='14' height='253' />";
		leftimages[24] = "<img src='http://www.dadabhagwan.org/images/banner/master_banner_left024.jpg' width='14' height='253' />";
		leftimages[25] = "<img src='http://www.dadabhagwan.org/images/banner/master_banner_left025.jpg' width='14' height='253' />";
		leftimages[26] = "<img src='http://www.dadabhagwan.org/images/banner/master_banner_left026.jpg' width='14' height='253' />";
		leftimages[27] = "<img src='http://www.dadabhagwan.org/images/banner/master_banner_left027.jpg' width='14' height='253' />";
		leftimages[28] = "<img src='http://www.dadabhagwan.org/images/banner/master_banner_left028.jpg' width='14' height='253' />";
		
		//alert(images[picnum]);
		if(document.getElementById('center'))
			document.getElementById('center').innerHTML = images[picnum];	
			
		if(document.getElementById('left_flashsub'))
			document.getElementById('left_flashsub').innerHTML = leftimages[picnum];	
}

function intro_rotate()
{
	var introimages = new Array(29);
		introimages[0] = "http://www.dadabhagwan.org/images/banner/Intro-Banners.jpg";							
		introimages[1] = "http://www.dadabhagwan.org/images/banner/Intro-Banners001.jpg";							
		introimages[2] = "http://www.dadabhagwan.org/images/banner/Intro-Banners002.jpg";
		introimages[3] = "http://www.dadabhagwan.org/images/banner/Intro-Banners003.jpg";							
		introimages[4] = "http://www.dadabhagwan.org/images/banner/Intro-Banners004.jpg";							
		introimages[5] = "http://www.dadabhagwan.org/images/banner/Intro-Banners005.jpg";							
		introimages[6] = "http://www.dadabhagwan.org/images/banner/Intro-Banners006.jpg";							
		introimages[7] = "http://www.dadabhagwan.org/images/banner/Intro-Banners007.jpg";							
		introimages[8] = "http://www.dadabhagwan.org/images/banner/Intro-Banners008.jpg";							
		introimages[9] = "http://www.dadabhagwan.org/images/banner/Intro-Banners009.jpg";							
		introimages[10] = "http://www.dadabhagwan.org/images/banner/Intro-Banners010.jpg";
		introimages[11] = "http://www.dadabhagwan.org/images/banner/Intro-Banners011.jpg";
		introimages[12] = "http://www.dadabhagwan.org/images/banner/Intro-Banners012.jpg";
		introimages[13] = "http://www.dadabhagwan.org/images/banner/Intro-Banners013.jpg";	
		introimages[14] = "http://www.dadabhagwan.org/images/banner/Intro-Banners014.jpg";
		introimages[15] = "http://www.dadabhagwan.org/images/banner/Intro-Banners015.jpg";
		introimages[16] = "http://www.dadabhagwan.org/images/banner/Intro-Banners016.jpg";
		introimages[17] = "http://www.dadabhagwan.org/images/banner/Intro-Banners017.jpg";
		introimages[18] = "http://www.dadabhagwan.org/images/banner/Intro-Banners018.jpg";
		introimages[19] = "http://www.dadabhagwan.org/images/banner/Intro-Banners019.jpg";
		introimages[20] = "http://www.dadabhagwan.org/images/banner/Intro-Banners020.jpg";
		introimages[21] = "http://www.dadabhagwan.org/images/banner/Intro-Banners021.jpg";
		introimages[22] = "http://www.dadabhagwan.org/images/banner/Intro-Banners022.jpg";
		introimages[23] = "http://www.dadabhagwan.org/images/banner/Intro-Banners023.jpg";
		introimages[24] = "http://www.dadabhagwan.org/images/banner/Intro-Banners024.jpg";
		introimages[25] = "http://www.dadabhagwan.org/images/banner/Intro-Banners025.jpg";
		introimages[26] = "http://www.dadabhagwan.org/images/banner/Intro-Banners026.jpg";
		introimages[27] = "http://www.dadabhagwan.org/images/banner/Intro-Banners027.jpg";
		introimages[28] = "http://www.dadabhagwan.org/images/banner/Intro-Banners028.jpg";
		
		if(document.getElementById('banner'))
			document.getElementById('banner').style.backgroundImage = "url("+introimages[picnum]+")";
}