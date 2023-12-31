
function startCounter() {
    // select the counter DOM and loop ove the counter and call a function
    $(".counter").each(function () {

        // define the counter selector with this
        var $this = $(this);

        //collect the counter number and store with a variable
        var counterNumber = $this.attr("data-count");

        //store the initial number to an object and animate with the counter number
        $({
            couterInit: $this.text()
        }).animate({
            couterInit: counterNumber
        }, {
                // animate function duration property
                duration: 500,
                //animatin function
                easing: 'linear',
                //animate function step property
                step: function () {
                    //output the counter number with calculating
                    $this.text(Math.ceil(this.couterInit));
                },
                // animate function compelte step
                complete: function () {
                    //show the last counter value
                    $this.text(this.counterNumbercountNum);
                }
            })

    });//end counter
}