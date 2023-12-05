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
	
	$id = $_POST["id"];
	$level = $_POST["level"];
	$tabledataquery = "UPDATE `networkingTest` SET `level`= " . $level . " WHERE id = " . $id;
	
	
	if(mysqli_query($con,$tabledataquery))
	{
		echo "[succes] updating level ($id), level: ($level).";
		exit();
	}
	else
		echo "[error] updating level ($id)";
?>
