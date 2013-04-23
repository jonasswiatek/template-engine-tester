<?php
  $input = file_get_contents("php://stdin");
  $render_desc = json_decode($input);
  
  $template_body = $render_desc -> TemplateBody;
  $Model = $render_desc -> Model;
  
  eval("?>" . $template_body . "<?php ");
?>