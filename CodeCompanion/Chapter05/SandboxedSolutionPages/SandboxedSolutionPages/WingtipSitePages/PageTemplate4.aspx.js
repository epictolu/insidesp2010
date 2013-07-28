
$(function () {

  // Accordion
  $("#accordion").accordion({ header: "h3" });

  // Tabs
  $('#tabs').tabs();


  // Dialog			
  $('#dialog').dialog({
    autoOpen: false,
    width: 600,
    buttons: {
      "Ok": function () {
        $(this).dialog("close");
      },
      "Cancel": function () {
        $(this).dialog("close");
      }
    }
  });

  // Dialog Link
  $('#dialog_link').click(function () {
    $('#dialog').dialog('open');
    return false;
  });

  $("#start").click(function () {
    $("#box").animate({ height: 200 }, "slow");
    $("#box").animate({ width: 250 }, "slow");
    $("#box").animate({ height: 300 }, "slow");
    $("#box").animate({ width: 400 }, "slow");
    $("#box").animate({ height: 400 }, "slow");
    $("#box").animate({ width: 550 }, "slow");    
    $("#box").animate({ height: 100 }, "slow");
    $("#box").animate({ width: 100 }, "slow");
  });

});