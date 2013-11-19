




/*************** REPLACE WITH YOUR OWN UA NUMBER Google analytics UA number probably ***********/
var UA = 'XXX';
/*************** REPLACE WITH YOUR OWN UA NUMBER ***********/


/*
|--------------------------------------------------------------------------
| DOCUMENT READY
|--------------------------------------------------------------------------
*/  

$(document).ready(function() {

    /***** form placeholder for IE *****/
    if(!Modernizr.input.placeholder){

        $('[placeholder]').focus(function() {
            var input = $(this);
            if (input.val() == input.attr('placeholder')) {
                input.val('');
                input.removeClass('placeholder');
            }
        }).blur(function() {
            var input = $(this);
            if (input.val() == '' || input.val() == input.attr('placeholder')) {
                input.addClass('placeholder');
                input.val(input.attr('placeholder'));
            }
        }).blur();
        $('[placeholder]').parents('form').submit(function() {
            $(this).find('[placeholder]').each(function() {
                var input = $(this);
                if (input.val() == input.attr('placeholder')) {
                    input.val('');
                }
            })
        });

    }									

    /*
    |--------------------------------------------------------------------------
    | FIXED MENU - HOMEPAGE
    |--------------------------------------------------------------------------
    */ 
    if($('#innerNav').length){
        $('#innerNav').waypoint('sticky');
    }

    $(window).resize(function() {
        $('.innerNavWrapperIcons ').removeClass('stuck');

    });

    

   /*
    |--------------------------------------------------------------------------
    | ONE PAGE NAV - HOMEPAGE
    |--------------------------------------------------------------------------
    */ 


    // Cache selectors
    var lastId,
    topMenu = $("#innerNav");
    topMenuHeight = topMenu.height(),
    menuItems = topMenu.find("a");

    // Anchors corresponding to menu items
    scrollItems = menuItems.map(function(){
      var item = $($(this).attr("href"));
      if (item.length) { return item; }
    });

    // Bind click handler to menu items
    // so we can get a fancy scroll animation
    menuItems.click(function(e){

        var href = $(this).attr("href"),
        offsetTop = href === "#" ? 0 : $(href).offset().top - topMenuHeight ;


        $('html, body').stop().animate({ 
          scrollTop: offsetTop
          
      }, 800);
        

        var id = $(this).attr('href'); 
        /*location.hash = id;*/

        return false;


    });


    // Bind to scroll
    $(window).scroll(function(){

       // Get container scroll position
       var fromTop = $(this).scrollTop() + topMenuHeight +10;
       
       // Get id of current scroll item
       var cur = scrollItems.map(function(){
         if ($(this).offset().top <= fromTop)
             return this;
       });



       // Get the id of the current element
       cur = cur[cur.length-1];
       var id = cur && cur.length ? cur[0].id : "";

       if (lastId !== id) {
         lastId = id;

           // Set/remove active class
           menuItems
           .removeClass("current")
           .filter("[href=#"+lastId+"]").addClass("current");
       } 

   });


    
    /*
    |--------------------------------------------------------------------------
    | PRETTY PHOTOS
    |--------------------------------------------------------------------------
    */      
    if( $("a.prettyPhoto").length){
        $("a.prettyPhoto").prettyPhoto({
            animation_speed:'fast',
            slideshow:10000, 
            hideflash: true
        });
    }
    
    
    /*
    |--------------------------------------------------------------------------
    | TOOLTIP
    |--------------------------------------------------------------------------
    */
    $('.tips').tooltip();
    
    /*
    |--------------------------------------------------------------------------
    | COLLAPSE
    |--------------------------------------------------------------------------
    */
    $('.accordion').on('show hide', function(e){
        $(e.target).siblings('.accordion-heading').find('.accordion-toggle i').toggleClass('icon-right-circle icon-down-circle', 200);
    });

    /*
    |--------------------------------------------------------------------------
    | CONTACT
    |--------------------------------------------------------------------------
    */   
    $('.slideContact').click(function(e){

        if ( $(window).width() >= 800){

            $('#contact').slideToggle('normal', 'easeInQuad',function(){

                $('#contactinfoWrapper').css('margin-left', 0);
                $('#mapSlideWrapper').css('margin-left', 3000);
                $('#contactinfoWrapper').fadeToggle();
                

            });
            $('#closeContact').fadeToggle(); 
            return false;
            
        }else{

            return true;
            
        }
    });
    
    
    $('#closeContact').click(function(e){


        $('#contactinfoWrapper').fadeOut('normal', 'easeInQuad',function(){
            $('#contactinfoWrapper').css('margin-left', 0);
            $('#mapSlideWrapper').css('margin-left', 3000);
        });
        
        $('#contact').slideUp('normal', 'easeOutQuad');

        $(this).fadeOut();

        e.preventDefault();
        
    });
    

    
    /* MAP */
    $('#mapTrigger').click(function(e){


        $('#mapSlideWrapper').css('display', 'block');
        initialize('mapWrapper');
        
        $('#contactinfoWrapper, #contactinfoWrapperPage').animate({
            marginLeft:'-2000px' 
        }, 400, function() {}); 
        
        
        $('#mapSlideWrapper').animate({
            marginLeft:'29px' 
        }, 400, function() {});  
        
        appendBootstrap();

        e.preventDefault();
    });
    
    
    $('#mapTriggerLoader').click(function(e){


        $('#mapSlideWrapper, #contactinfoWrapperPage').css('display', 'block');

        $('#contactinfoWrapper, #contactinfoWrapperPage').animate({
            marginLeft:'-2000px' 
        }, 400, function() {}); 
        
        
        $('#mapSlideWrapper').animate({
            marginLeft:'29px' 
        }, 400, function() {});  

        
        appendBootstrap();
        
        e.preventDefault();
    });
    
    
    $('#mapReturn').click(function(e){
        //$('#mapWrapper').css('margin-bottom', '3em');
        

        $('#mapSlideWrapper').animate({
            marginLeft:'3000px' 
        }, 400, function() {});       
        

        $('#contactinfoWrapper, #contactinfoWrapperPage').animate({
            marginLeft:'0' 
        }, 400, function() {
            $('#mapSlideWrapper').css('display', 'none');
        }); 

        e.preventDefault();
    }); 



    /*
    |--------------------------------------------------------------------------
    | FLEXSLIDER
    |--------------------------------------------------------------------------
    */ 
    if($('.flexslider').length){
        $('.flexslider').flexslider({
            animation: "slide",
            controlNav: false,
            directionNav: true,
            slideshow: false,
            start: function(slider){
                setTimeout("animateTxt("+slider.currentSlide+", 'in')", 100);  
            },
             before: function(slider){
                setTimeout("animateTxt("+slider.currentSlide+")", 100);  
            },
            after: function(slider){
                setTimeout("animateTxt("+slider.currentSlide+", 'in')", 100);  
            } 
        });

    }






    /*
    |--------------------------------------------------------------------------
    | ROLLOVER BTN
    |--------------------------------------------------------------------------
    */ 

    $('.socialIcon').hover(
        function () {
            $(this).stop(true, true).addClass('socialHoverClass', 300);
        },
        function () {
            $(this).removeClass('socialHoverClass', 300);
        });





    $('.tabs li, .accordion h2').hover(
        function () {
            $(this).stop(true, true).addClass('speBtnHover', 300);
        },
        function () {
            $(this).stop(true, true).removeClass('speBtnHover', 100);
        });



    

    /*
    |--------------------------------------------------------------------------
    | ALERT
    |--------------------------------------------------------------------------
    */ 
    $('.alert').delegate('button', 'click', function() {
        $(this).parent().fadeOut('fast');
    });
    
    
    /*
    |--------------------------------------------------------------------------
    | CLIENT
    |--------------------------------------------------------------------------
    */   
    
    if($('.colorHover').length){
        var array =[];
        $('.colorHover').hover(

            function () {

                array[0] = $(this).attr('src');
                $(this).attr('src', $(this).attr('src').replace('-off', ''));

            }, 

            function () {

                $(this).attr('src', array[0]);

            });
    }


    /*
    |--------------------------------------------------------------------------
    | UP AND DOWN & MENU BTNS PORTFOLIO STATIC
    |--------------------------------------------------------------------------
    */ 

    $('.goDown').click(function(e){

        var offset = $(this).parents().next('section').offset();
        var variation = ($('.navbar-fixed-top').length)?$('.navbar-fixed-top').outerHeight(true) +20 :90;
        var finalPos  = offset.top - variation;
        
        scrollTo(finalPos, 500);
        e.preventDefault();
        
    });


    $('.goUp').click(function(e){

        var offset = $(this).parents().prev('section').offset();
        var variation = ($('.navbar-fixed-top').length)?$('.navbar-fixed-top').outerHeight(true) +20:90;
        var finalPos  = offset.top - variation;
        
        scrollTo(finalPos, 500);
        e.preventDefault();
    }); 
    

    $('.PortfolioStickyMenu ul li a').click(function(e){


        var targetId =  $(this).attr('href');
        var offset = $(targetId).offset() ;
        var variation = ($('.navbar-fixed-top').length)?$('.navbar-fixed-top').outerHeight(true) +20:90;
        var finalPos  = offset.top - variation;

        scrollTo(finalPos , 500); 
        e.preventDefault();

        
    });
    
    /*
    |--------------------------------------------------------------------------
    | CAMERA SLIDER
    |--------------------------------------------------------------------------
    */ 

    if($('.camera_wrap').length){

        jQuery('.camera_wrap').camera({
            thumbnails: true,
            pagination: true,
            height:'506'
        });

    }

    if($('.camera_wrap_nonav').length){

        jQuery('.camera_wrap_nonav').camera({
            pagination: false,
            thumbnails: true,
            height:'70%'
        });

    } 
     
    if($('.camera_wrap_nothumb').length){

        jQuery('.camera_wrap_nothumb').camera({
            pagination: false,
            thumbnails: false,
            height:'70%'
        });

    }  


    /*
    |--------------------------------------------------------------------------
    | ISOTOPE USAGE FILTERING
    |--------------------------------------------------------------------------
    */ 
    if($('#banner').length){

        
        $('#banner').oneByOne({
            className: 'oneByOne1',              
            width: '100%',
            height: 420,
            easeType: 'random',
            slideShow: true,
            responsive: true,
            minWidth: 480,
            autoHideButton: true
        });
        $('#banner').css('display', 'block');
        $('.oneByOne1').slideDown('normal');
        
    }

//END DOCUMENT READY   
});



/*
|--------------------------------------------------------------------------
| EVENTS TRIGGER AFTER ALL IMAGES ARE LOADED
|--------------------------------------------------------------------------
*/
$(window).load(function() {


    /*
    |--------------------------------------------------------------------------
    | DIRECTIONAL ROLLOVER AND HOVER EFFECTS
    |--------------------------------------------------------------------------
    */     

    
    if($('.presBloc').length){


        $('.presBloc .imgWrapper').hover(

            function () {
                var $this = $(this);


                $('.mask', $this).css('height', $this.outerHeight());
                $('.media-hover', $this).css('height', $this.outerHeight());
                $('.media-hover', $this).stop().fadeIn('fast').end();

                if($('.mask>i').length){
                    var centerX = $this.outerWidth()/2 - $('.mask>i', $this).outerWidth(true)/2;
                    var centerY = $this.outerHeight()/2 - $('.mask>i', $this).outerHeight(true)/2;
                    $('.mask>i', $this).css('top', centerY);
                    $('.mask>i', $this).css('left', centerX);
                }

                if($('.mask>.insideLinkHolder').length){
                  var centerX = $this.outerWidth()/2 - $('.mask>.insideLinkHolder', $this).outerWidth(true)/2;
                  var centerY = $this.outerHeight()/2 - $('.mask>.insideLinkHolder', $this).outerHeight(true)/2;
                  $('.mask>.insideLinkHolder', $this).css('top', centerY);
                  $('.mask>.insideLinkHolder', $this).css('left', centerX);
                }
   
            }, 
            function () {
                var $this = $(this);
                $('.media-hover', $this).stop().fadeOut('fast').end();
     
            });
    }

    /*
    |--------------------------------------------------------------------------
    | ISOTOPE USAGE FILTERING
    |--------------------------------------------------------------------------
    */ 
    if($('.isotopeWrapper').length){

        var $container = $('.isotopeWrapper');
        var $resize = $('.isotopeWrapper').attr('id');
        // initialize isotope
        
        $container.isotope({
            itemSelector: '.isotopeItem',
            resizable: false, // disable normal resizing
            masonry: {
                columnWidth: $container.width() / $resize
            }


            
        });

        $('#filter a').click(function(){
            $('#filter a').removeClass('current');
            $(this).addClass('current');
            var selector = $(this).attr('data-filter');
            $container.isotope({
                filter: selector
            });
            return false;
        });
        
        
        $(window).smartresize(function(){
            $container.isotope({
                // update columnWidth to a percentage of container width
                masonry: {
                    columnWidth: $container.width() / $resize
                }
            });
        });
        
        $container.delegate('.masoneryBloc .imgWrapper', 'click', function(){

            var $this = $(this);

            if($this.parent().hasClass('span4')){

               $('.masoneryBloc').addClass('span4');
               $('.masoneryBloc').removeClass('span8');

               $this.parent().removeClass('span4');
               $this.parent().addClass('span8');
               $this.parent().find('.media-hover').css('display', 'none');
               
               if($('.hiddenInfos').length){

                 $('.masoneryBloc').find('.mask>i').attr('class', 'icon-plus');
                 $this.find('.mask>i').attr('class', 'icon-minus');
                 $('.hiddenInfos').css('display', 'none');
                 $this.parent().children('.hiddenInfos').css('display', 'block');

               }
                
            }else{

                $this.parent().addClass('span4');
                $this.parent().removeClass('span8');
                $this.parent().find('.media-hover').css('display', 'none');
                $this.find('.mask>i').attr('class', 'icon-plus');

                if($('.hiddenInfos').length){

                    $('.hiddenInfos').css('display', 'none');

                }
                
            }


            
            $container.isotope('reLayout');
            return false;
        });
}  


if($('#testimonialCarousel').length){
 $('#testimonialCarousel').carousel('cycle');
}




//END WINDOW LOAD
});


/*
|--------------------------------------------------------------------------
| FUNCTIONS
|--------------------------------------------------------------------------
*/


/* CONTACT FROM */

jQuery(function() {
    if(jQuery("#contactfrm").length){

        /*$('#name').prop('minlength', 2);
        $('#comments').prop('minlength', 10);*/

        // show a simple loading indicator
        var loader = jQuery('<div id="loader"><img src="~/images/loading.gif" alt="loading..." /></div>')
        .css({
          position: "relative", 
          top: "1em", 
          left: "25em", 
          display: "inline"
      })
        .appendTo("body")
        .hide();
        jQuery().ajaxStart(function() {
          loader.show();
      }).ajaxStop(function() {
          loader.hide();
      }).ajaxError(function(a, b, e) {
          throw e;
      });

      var v = jQuery("#contactfrm").validate({
          // debug: true,
          errorPlacement: function(error, element) {
            error.insertBefore( element );
        },
        submitHandler: function(form) {
            jQuery(form).ajaxSubmit({
              target: ".result"
          });
        },
        rules: {
            name: {
                required: true,
                minlength: 3
            },
            email: {
                required: true,
                email: true
            },
            phone: {
                required: true,
                minlength: 10,
                digits:true
            },
            comment: {
                required: true,
                minlength: 10,
                maxlength: 350
            }
        }
    });
  }

});

/* CONTACT FROM */


/* FLEXSLIDER INNER INFO CUSTOM ANIMATION */
function animateTxt(curSlide, action){

    if(action == 'in'){
        var i = 0;
        var animaDelay = 0;

        $('.slideN'+curSlide+':not([class*=clone])>.caption').css('display', 'block');

        $('.slideN'+curSlide+':not([class*=clone])>.caption>div').each(function( ) {
            if(Modernizr.csstransitions) { 
               
                $(this).css('-webkit-animation-delay', animaDelay+'s');
                $(this).css('-moz-animation-delay', animaDelay+'s');
                $(this).css('-0-animation-delay', animaDelay+'s');
                $(this).css('-ms-animation-delay', animaDelay+'s');
                $(this).css('animation-delay-delay', animaDelay+'s');

                $(this).show().addClass('animated').addClass($(this).attr('data-animation'));

            }else{

                $('.slideN'+curSlide+':not([class*=clone])>.caption>div').hide();
                if (i == 0){timing = 0}else if(i == 1){timing = 300} else{ timing = 300 * i}
                $(this).delay(timing).fadeIn('fast');
            }
            i++;
            animaDelay = animaDelay+0.2;


        });

    }else{
        var j = 0;
        $('.slideN'+curSlide+':not([class*=clone])>.caption').css('display', 'none');

        $('.slideN'+curSlide+':not([class*=clone])>.caption>div').each(function( ) {
         if(Modernizr.csstransitions) { 

             $(this).removeClass($(this).attr('data-animation')).removeClass('animated').hide();

         }else{
            $(this).hide();
        }
        j++;
    });
    }

}




/* MAIN MENU (submenu slide and setting up of a select box on small screen)*/
(function() {

    var $mainMenu = $('#mainMenu').children('ul');

    $mainMenu.on('mouseenter', 'li', function() {


        var $this = $(this),
        
        $subMenu = $this.children('ul');


        if( $subMenu.length ) $this.addClass('hover');
        else {
            if($this.parent().is($(':gt(1)', $mainMenu))){
                $this.addClass('Shover').stop().hide().fadeIn('fast').end();
            }else{
                $this.addClass('Shover');
            }
        }


        if($this.parent().is($(':gt(1)', $mainMenu))){

            $subMenu.css('display', 'block');
            $subMenu.stop(true, true).animate({
                left:180, 
                opacity:1
            }, 300,'easeOutQuad');



            
        }else{

            $subMenu.stop(true, true).slideDown('fast','easeInQuad'); 
            
        }


    }).on('mouseleave', 'li', function() {


        var $nthis = $(this);
        if($nthis.parent().is($(':gt(1)', $mainMenu))){

            $nthis.children('ul').css('left', 130).css('opacity', 0).css('display', 'none');

        }else{

            $nthis.removeClass('hover').removeClass('Shover').children('ul').stop(true, true).hide();
        }
        
        $subMenu = $nthis.children('ul');
        
        if( $subMenu.length ) $nthis.removeClass('hover');
        else $nthis.removeClass('Shover');
        
        
    }).on('touchend', 'li ul li a', function(e) {

        var el = $(this);
        var link = el.attr('href');
        window.location = link;
        
    });

    
    // ul to select
    var optionsList = '<option value="" selected>Navigate...</option>';
    $mainMenu.find('li').each(function() {
        var $this   = $(this),
        $anchor = $this.children('a'),
        depth   = $this.parents('ul').length - 1,
        indent  = '';

        if( depth ) {
            while( depth > 0 ) {
                indent += ' - ';
                depth--;
            }
        }

        optionsList += '<option value="' + $anchor.attr('href') + '">' + indent + ' ' + $anchor.text() + '</option>';
    }).end().after('<select class="responsive-nav">' + optionsList + '</select>');

    $('.responsive-nav').on('change', function() {
        window.location = $(this).val();
    }); 

})();


/* BACK TO TOP (BTN back to top by Matt Varone)*/
(function() {

    var defaults = {
        btnText: '<i class="icon-up-open"></i>',
        min: 50,
        inDelay:600,
        outDelay:400,
        containerID: 'to-top',
        /*containerCLASS: '',*/
        containerHoverID: 'to-top',
        scrollSpeed: 300,
        easingType: 'linear'
    },
    settings = $.extend(defaults),
    containerIDhash = '#' + settings.containerID,
    containerHoverIDHash = '#'+settings.containerHoverID;

    $('#mainFooter').append('<a href="#" id="'+settings.containerID+'" class="'+settings.containerCLASS+'">'+settings.btnText+'</a>');
    $(containerIDhash).hide().on('click.UItoTop',function(){
        $('html, body').animate({
            scrollTop:0
        }, settings.scrollSpeed, settings.easingType);
        $('#'+settings.containerHoverID, this).stop().animate({
            'opacity': 0
        }, settings.inDelay, settings.easingType);
        return false;
    })
    //.prepend('<span class="'+settings.containerHoverID+'"></span>')
    .hover(function() {

        $(containerHoverIDHash, this).stop().animate({
            'opacity': 1
        }, 600, 'linear');
    }, function() { 


        $(containerHoverIDHash, this).stop().animate({
            'opacity': 0
        }, 700, 'linear');
    });

    $(window).scroll(function() {
        var sd = $(window).scrollTop();
        if(typeof document.body.style.maxHeight === "undefined") {
            $(containerIDhash).css({
                'position': 'absolute',
                'top': sd + $(window).height() - 50
            });
        }
        if ( sd > settings.min ) 
            $(containerIDhash).fadeIn(settings.inDelay);
        else 
            $(containerIDhash).fadeOut(settings.Outdelay);
    });

})();


/*
|--------------------------------------------------------------------------
| SIDEBAR MENU FOLLOWING WINDOW SCROLL
|--------------------------------------------------------------------------
*/             

function scrollTo($position, $animationTime){

    $('html,body').animate({
        scrollTop: $position
    }, $animationTime);
    
}

/*
|--------------------------------------------------------------------------
| STICKY MENU
|--------------------------------------------------------------------------
*/   

$(function() {
    $window  = $(window);
    if( $(".PortfolioStickyMenu").length && $window.width() >= 754) {
        var $sidebar   = $(".PortfolioStickyMenu"), 

        offset     = $sidebar.offset(),
        topPadding = 108;


        $window.scroll(function() {
            if ($window.scrollTop() > offset.top) {
                $sidebar.stop().animate({
                    marginTop: $window.scrollTop() - offset.top + topPadding
                });
            } else {
                $sidebar.stop().animate({
                    marginTop: 0
                });
            }
        });
    }
    
});


/*
|--------------------------------------------------------------------------
| GOOGLE MAP
|--------------------------------------------------------------------------
*/

function appendBootstrap() {
    var script = document.createElement("script");
    script.type = "text/javascript";
    script.src = "http://maps.google.com/maps/api/js?sensor=false&callback=initialize";
    document.body.appendChild(script);
}    


function initialize( id ) {

  



}
/*
|--------------------------------------------------------------------------
| SHARRRE
|--------------------------------------------------------------------------
*/
if($('#shareme').length){
    $('#shareme').sharrre({
      share: {
        googlePlus: true,
        facebook: true,
        twitter: true,
    },
    buttons: {
        googlePlus: {size: 'tall'},
        facebook: {layout: 'box_count'},
        twitter: {count: 'vertical'},    
    },
    enableHover: false,
    enableCounter: false,
    enableTracking: true,
      //url:'document.location.href'
  });
}