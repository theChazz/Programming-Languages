<?php
//connection to database
include("includes/db_connection.php");
$obj = new db_connection();

//saving back end validation sessions
session_start();

if(isset($_POST['insert_product']))
	{
		//mysqli_real_escape_string helps prevent SQL injection from form
		$SKU 	= mysqli_real_escape_string($db_connection,$_POST['sku']);
		$name 	= mysqli_real_escape_string($db_connection,$_POST['name']);
		$price 	= mysqli_real_escape_string($db_connection,$_POST['price']);
		$type 	= mysqli_real_escape_string($db_connection,$_POST['product_type']);
		$size 	= mysqli_real_escape_string($db_connection,$_POST['size']);  
		$height = mysqli_real_escape_string($db_connection,$_POST['height']); 
		$length = mysqli_real_escape_string($db_connection,$_POST['length']); 
		$width 	= mysqli_real_escape_string($db_connection,$_POST['width']); 
		$weight = mysqli_real_escape_string($db_connection,$_POST['weight']); 
		$attribute = "test";

		//Created an array to check for errors 
		$errors = array();
		if (empty($SKU))     		{   $errors[] = 'You forgot to enter the sku.'; }
		if (empty($name))     		{   $errors[] = 'You forgot to enter the product name.'; }
		if (empty($price))     		{   $errors[] = 'You forgot to enter the product price.'; }
		if (empty($type))     		{   $errors[] = 'You forgot to enter the product type.'; }

		//Check if product has already been captured
		// *** NO CODE ***

		//Check if fields are not empty
		if (empty($errors)) 
		{ 
			// If everything's OK.
			//Insert or add new product in database
			if($type == 1 )
			{
				//Created an array to check for errors 
				// *** NO CODE ***
				
				
				$add_query = $obj->insert_new_product($SKU, $name, $price, $size, $type);
				header ("location: product_list_page.php?success");
				exit();
			}
			else if($type == 3 )
			{
				//Created an array to check for errors 
				// *** NO CODE ***
				
				//(HxWxL)
				$attribute = "{$height}x{$width}x{$length}";
				$add_query = $obj->insert_new_product($SKU, $name, $price, $attribute, $type);
				header ("location: product_list_page.php?success");
				exit();
			}
			else if($type == 2)
			{
				//Created an array to check for errors 
				// *** NO CODE ***
				
				
				$add_query = $obj->insert_new_product($SKU, $name, $price, $weight, $type);
				header ("location: product_list_page.php?success");
				exit();
			}
			else 
			{
				// If it did not run OK.
				$_SESSION['error_array'] = $errors;
				header ("location: product_add_page.php?error");
				exit();
			}
		}
		else 
		{
			// If it did not run OK.
			$_SESSION['error_array'] = $errors;
			header ("location: product_add_page.php?error");
			exit();
		}
    			
    }

?>


