<?php
    function webdsl_toArray($obj){
        if (!$obj) return Array();
        
        if (is_array($obj)) return $obj;
            
        if (is_object($obj)) return Array($obj);
    }
    
    function webdsl_isFalseNullOrEmpty($obj){
        if (value == null | !value | value.length == 0) return true;
        return false;
    }
?>