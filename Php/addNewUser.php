<?php
header("Access-Control-Allow-Credentials: true");
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
header('Access-Control-Allow-Headers: Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time');

	$con = mysqli_connect('localhost', 'u635035423_razi', 'PpSp@Wz.BDDVG8j', 'u635035423_Digipen');

	//check that connection happened
	if(mysqli_connect_errno())
	{
		echo "1: Connection failed"; //error code #1 = connection failed
		exit();
	}
	
	$name = $_POST["name"];
	$email = $_POST["email"];
	$insertuserquery = "INSERT INTO `networkingTest` (name, email) VALUES ('" . $name . "', '" . $email . "');";
	
	if(mysqli_query($con,$insertuserquery))
	{
		echo "[succes] adding data ($name), email: ($email).";
		exit();
	}
	else
		echo "[error] adding data ($name)";
?>
