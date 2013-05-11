<?php include 'webdslframework.php'; ?>
Hello <?= $Model->name ?>
You have just won $<?= $Model->value ?>!
<?php foreach (webdsl_toArray($Model->in_ca) as $item1) { ?> 
Well, $<?= $item1->taxed_value ?>, after taxes.
<?php } ?>