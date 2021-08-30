(function ($) {
    $(document).ready(function () {
        $('#menu').click(function (event) {
            //if ($(event.target).closest('aside#mySidenav').length === 0 && $(event.target).closest('div#subopcionesmenu').length === 0 && $(event.target).closest('.sidebar-toggle').length === 0) {
            //    closeNav()
            //}
            //else {
            //    openNav()
            //}
            //debugger
            var val = $("#mySidenav").hasClass("menu-inicial");
            if (val == true) {
                $("#subopcionesmenu").removeClass("menu-inicial");
                $("#mySidenav").removeClass("menu-inicial");
                //$("#mySidenav").css("display", "block");
            } else {
                $("#subopcionesmenu").addClass("menu-inicial");
                $("#mySidenav").addClass("menu-inicial");
                $(".subopcionesmenu").hide();
                $(".subopciones").hide();
                //$("#mySidenav").css("display", "none");
            }
        })
        //$('#menu').click(function (e) {
        //    if ($('#sidebar-collapse').is(':hidden')) {
        //        $("#sidebar-collapse").fadeToggle(1000);
        //        if ($("#clase").is(":visible")) {
        //            $('#clase').hide();
        //        }
        //    }

        //    else {
        //        $(".subopcionesmenu").hide();
        //        $(".subopciones").hide();
        //        $('#clase').hide();
        //    }
        //});

        //$("#menu")
        //    .mouseover(function () {
        //        $("#cssmenu").fadeToggle(1000);
        //    })
        //    .mouseout(function () {

        //    });

        //$("#container")
        //    .mouseover(function () {
        //        $(".subopcionesmenu").hide();
        //        $(".subopciones").hide();
        //        $('#cssmenu').hide();
        //    });

        //$("footer")
        //    .mouseover(function () {
        //        $(".subopcionesmenu").hide();
        //        $(".subopciones").hide();
        //        $('#cssmenu').hide();
        //    });



        $("#container")
            .click(function () {
                $(".subopcionesmenu").hide();
                $(".subopciones").hide();
                $('#clase').hide();
            });

        $("#footer")
            .click(function () {
                $(".subopcionesmenu").hide();
                $(".subopciones").hide();
                $('#clase').hide();
            });

        $('#cuenta').click(function (e) {
            if ($("#info_cuenta").is(":visible"))
                $('#info_cuenta').hide();

            else
                $('#info_cuenta').fadeToggle(1000);
            $(".subopcionesmenu").hide();
            $(".subopciones").hide();
            $('#clase').hide();
        });

        $('#cuentaa').click(function (e) {
            if ($("#info_cuenta").is(":visible"))
                $('#info_cuenta').hide();

            else
                $('#info_cuenta').fadeToggle(1000);
            $(".subopcionesmenu").hide();
            $(".subopciones").hide();
            $('#clase').hide();
        });

        $('#moda').click(function (e) {
            if ($("#info_cuenta").is(":visible"))
                $('#info_cuenta').hide();

            else
                $('#info_cuenta').fadeToggle(1000);
            $(".subopcionesmenu").hide();
            $(".subopciones").hide();
            $('#clase').hide();
        });

        $('#container').click(function (e) {
            $('#clase').hide();
            $('#info_cuenta').hide();
            $(".subopcionesmenu").hide();
            $(".subopciones").hide();
        });
        $('#bnt-abrir-calendario').click(function (e) {

            $('#clase').hide();
            $('#info_cuenta').hide();
            $(".subopcionesmenu").hide();
            $(".subopciones").hide();
        });
    });
})(jQuery);

