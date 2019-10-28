// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

  $(document).ready(function () {
    // show or hide entry form every time the button is checked or unchecked
    $('#addEntryCheck').change(function(){
      if($(this).prop("checked")) {
        $('#entryForm').show();
      } else {
        $('#entryForm').hide();
      }
    });

    // display form after postback
    if($('#addEntryCheck').prop("checked")) {
      $('#entryForm').show();
    }

    // display table after short delay
    setTimeout(function(){
      $('#entryTable').show();
    }, 100);

    // character counter
    $("#txtEntry").charcount({
      errFontColor:'red',
      okFontColor:'green',
      FontWeight:'bold',
      NumOfCharOfAlert: 20,
      isAlwaysOn:true
    });
});
  
  

//Get the button:
mybutton = document.getElementById("topButton");
// When the user clicks on the button, scroll to the top of the document
function topFunction() {
    document.body.scrollTop = 0; // For Safari
    document.documentElement.scrollTop = 0; // For Chrome, Firefox, IE and Opera
}