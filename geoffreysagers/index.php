<!doctype html>
<html>
<head>
<meta charset="utf-8">
<title>Perpetual 2014</title>
<link href="css/styles.css" rel="stylesheet" type="text/css">
<?php include ("functions.php"); ?>
</head>
<body>
<div class="input">
	<h3>Perpetual 2014</h3>
	<p>This form will convert an integer from 1721426 to 2914695 to its respective date. However, after December 31, 2014 additional months will just be added.</p>
	<form method="post" action="result.php">
			<label>Enter integer</label><br>
			<input type="number" name="stringtoparse" id="stringtoparse" min="1721426" max="2914695" value="2457023"><br>
			<input type="submit" name="Submit" value="Submit"/>
	</form>
</div>
</body>
</html>
