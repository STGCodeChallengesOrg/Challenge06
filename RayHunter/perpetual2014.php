#!/usr/bin/env php
<?php

/**
 * STG Code Challenge - Perpetual 2014
 *
 * @author Ray Hunter ray.hunter@stgconsulting.com
 */
function convert_to_perpetual_date($jd) {

	// check for date range
	if($jd > 2914695 && $jd < 1721426) {
		echo "Please enter a Julian date between 1721426 and 2914695.";
		exit(0);
	}
	
	// perpetual year
	$perpetual_year = 2014;

	// Convert the date to Gregorian and then parse the date.
	$date = date_parse(jdtogregorian($jd));
	$month = $date['month'];	
	$year = $date['year'];
	
	// check if the year is greater than the perpetual year
	if($date['year'] > $perpetual_year) {
		$month += (12 * ($date['year'] - $perpetual_year));
		$year = $perpetual_year;
	}	
	
	// print out the dates
	echo "Julian Date: " . $jd . "\t";	
	echo sprintf("Perpetual 2014: %d-%02d-%02d", $year, $month, $date['day']) . "\n";
}


// process the standard input
$f = fopen('php://stdin', 'r');
while($jd = fgets($f)) {
	convert_to_perpetual_date((int)$jd);
}
fclose($f);
