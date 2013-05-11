<?php include 'webdslframework.php'; ?>
<html>
  <title><?= $Model->PageTitle ?></title>
  <body>
	<h1>People</h1>
	<ul>
    <?php foreach (webdsl_toArray($Model->People) as $item1) { ?> 
	    <li>
		    <div>Name: <?= $item1->Name ?></div>
			<div>Age: <?= $item1->Age ?></div>
		</li>
	<?php } ?>
  </body>
</html>