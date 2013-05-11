<?php include 'webdslframework.php'; ?>
<?php foreach (webdsl_toArray($Model->wrapped) as $item1) { ?> 
  <?= $item1->name ?> is awesome.
<?php } ?>