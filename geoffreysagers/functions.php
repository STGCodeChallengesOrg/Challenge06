<?php
function getLeapYears(){
	$leapyears = [];
	for ($x=2016;$x<=3268;$x++){
		
		if (($x % 4 == 0) && ($x % 100 != 0)){
		$leapyears[]=$x;
		}
		elseif($x % 400 == 0){
			$leapyears[]=$x;
			}
		}
	return($leapyears);
	}


function parseInteger($stringtoparse){
	//1721426 = 1/1/1
	//2457023 = 12/31/2014
	//2457062 = 02/08/2015
	//2914695  = 1/23/3268
	$leapyears = getLeapYears();
	if ($stringtoparse <= 2457023){
	$newdate = cal_from_jd($stringtoparse, CAL_GREGORIAN);
	
	if ($newdate["month"] < 10) {
		$newdate["month"] = "0" . $newdate["month"];
		}
	if ($newdate["day"] < 10) {
		$newdate["day"] = "0" . $newdate["day"];
		}		
		return($newdate["day"] . "/" . $newdate["month"] . "/" . $newdate["year"]);
	}
	else{
	$newdate = cal_from_jd($stringtoparse, CAL_GREGORIAN);
	$position = 0;
	for($y=2016;$y<=$newdate["year"]; $y++){
		$position = array_search($y,$leapyears)+1;
		}
	$stringtoparse = $stringtoparse + $position;
	$newdate = cal_from_jd($stringtoparse, CAL_GREGORIAN);	
	$newdate["month"] = $newdate["month"] + (($newdate["year"]-2014)*12);
	$newdate["year"] = 2014;
	return($newdate["day"] . "/" . $newdate["month"] . "/" . $newdate["year"]);
	}
	}	


?>
