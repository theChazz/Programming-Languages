<?php 

//connection to database
include("includes/db_connection.php");
$obj = new db_connection();

//return query from db_connection
$query = $obj->fetch_all_poducts();

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
        <button class="" > <a href="product_add_page.php" class="">ADD</a> </button>
        <button class="" id="massDelete"> MASS DELETE </button>
    </div>
    
  </div>

  <hr>

  <form method="post" name="delete_products" action="process_products_delete.php">
  <div class="row">

    <div class="col-md-12">
	</div>
	  
		<!-- Products - start -->
		<?php
		if(!empty($query))
		{
			while($result = mysqli_fetch_assoc($query))
			{
				$id = $result['id'];
				$sku = $result['sku'];
				$name = $result['name'];
				$price = $result['price'];
				$product_type = $result['product_type'];
				
				if($product_type == 3)
				{ $attribute = 'Dimension: '.$result['attribute']; }
				else if($product_type == 1)
				{ $attribute = 'Size: '.$result['attribute'].' MB'; }
				else if($product_type == 2)
				{ $attribute = 'Weight: '.$result['attribute'].' KG'; }
				else
				{ $attribute = 'Other attribute: '.$result['attribute']; }
				

				echo '<div class="solid col-md-3 pb-12 text-center">'.
					
					'<input class="delete-checkbox" type="checkbox" id="'.$id.'" name="delete[]" value="'.$id.'"> <br>'.
					$name .' <br> '. $sku .' <br> '. $price.' $ <br> '
					
					. $attribute.' <br> '
					
					.'</div>';

			}
		}
		else
		{
			echo "No products found";
		}
		?>
		<!-- Products - end -->
		
		<button hidden name="delete_products" id="delete" type="submit" class="btn btn-danger">Delete</button> 

  </div>
  </form>
	  
  <hr>

</div>

  
<script src="js/delete_product.js"></script>

	
</body>
</html>