<?php include 'webdslframework.php'; ?>
<?php foreach (webdsl_toArray($Model->repo) as $item1) { ?> 
  <b><?= $item1->name ?></b>
<?php } ?>
<?php if(webdsl_isFalseNullOrEmpty($Model->repo)) { ?>
  No repos :(
<?php } ?>