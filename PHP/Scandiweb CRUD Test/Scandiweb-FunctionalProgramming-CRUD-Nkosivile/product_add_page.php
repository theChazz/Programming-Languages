<?php 

//connection to database
include("includes/db_connection.php");
$obj = new db_connection();

//return query from db_connection
$query = $obj->fetch_all_poducts();

//just incase javascript validation is not working
session_start();
if(!empty($_SESSION['error_array'])){
$errors = $_SESSION['error_array'];
}

?>

<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Product List</title>

  <!-- JS & Bootstrap from the internet-->
  <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.3.1/dist/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
	
	<!-- Bootstrap from the internet-->
  	<style>
	div.solid 
		{
			border-style: solid;
		}
	</style>	

</head>
<body>

<div class="container">

  <div class="row">

    <div class="col-md text-left">
      <h3>Product List</h3> 
    </div>

    <div class="col-md text-right">
        <button id="save">SAVE</button>
        <button><a href="product_list_page.php" class="">CANCEL</a></button>
    </div>
    
  </div>

  <hr>

  <div class="row">
	<!-- BACK END VALIDATION SET TO OFF - start -->
	<?php
	/*
	if(!empty($errors))
	{
	
		// Report the errors.
		$errorstring = "We apologize for any inconvenience! <br /> The following error(s) occurred:<br>";
		foreach ($errors as $msg) 
	{ 
		// Print each error.
		$errorstring .= " - $msg<br>\n";
	}
		$errorstring .= "Please try again.<br>"; 
		echo "<p class=' text-center col-sm-12' style='color:red'>$errorstring</p>
		"
		;	    
	}
	else
	{
		echo 'Errors where not found';
	}
	*/
	?>
	<!-- BACK END VALIDATION SET TO OFF - end -->
	  
	<!-- Product Add Form - start -->
    <div class="col-md-12">
		
	  	<form method="post" name="insert_product" action="process_product_add.php">
			
            <div class="form-group row">
                <label class="col-sm-2 col-form-label" for="sku">SKU</label>
                <input type="text" class="col-sm-4 form-control" name="sku" id="sku" required>
            </div>
			
            <div class="form-group row">
                <label class="col-sm-2 col-form-label" for="name">Name</label>
                <input type="text" class="col-sm-4 form-control" name="name" id="name" required>
            </div>
			
            <div class="form-group row">
                <label class="col-sm-2 col-form-label" for="price">Price ($)</label>
                <input type="number" step="0.01" class="col-sm-4 form-control" name="price" id="price" required>
            </div>
			
            <div class="form-group row">
                <label class="col-sm-2 col-form-label" for="type">Type</label>
                <select class="col-sm-4 form-control" id="product_type" name="product_type" id="product_type" required>
                <option value='1'>DVD-disc</option>
                <option value='2'>Book</option>
                <option value='3'>Furniture</option>
                </select>
            </div>
			
            <div class="form-group row" id="attribute_1">
                <label class="col-sm-2 col-form-label" for="size">Size (MB)</label>
                <input class="col-sm-4 form-control" type="number"  name="size" id="size" >
            </div>
			
			<div class="" id="attribute_2">
				
				<div class="form-group row">
				<label class="col-sm-2 col-form-label" for="size">Height (CM)</label>
                <input class="col-sm-4 form-control" type="number"  name="height" id="height" >
				</div>
				<div class="form-group row">
				<label class="col-sm-2 col-form-label" for="size">Width (CM)</label>
                <input class="col-sm-4 form-control" type="number"  name="width" id="width" >
				</div>
				<div class="form-group row">
				<label class="col-sm-2 col-form-label" for="size">Length (CM)</label>
                <input class="col-sm-4 form-control" type="number"  name="length" id="length" >
				</div>
                
				
				
            </div>
			
			<div class="form-group row" id="attribute_3">
                <label class="col-sm-2 col-form-label" for="size">Weight (KG)</label>
                <input type="number" class="col-sm-4 form-control" name="weight" id="weight" >
            </div>
			
            <button hidden name="insert_product" id="submit" type="submit" class="btn btn-primary">Submit</button>
            
	  </form>
		<p id="para"></p>
	</div>	
   	<!-- Product Add Form - end -->

  </div>

  <hr>

</div>

  
<script src="js/add_product.js"></script>
	

</body>
</html>