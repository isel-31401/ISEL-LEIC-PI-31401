/*
    Variables
*/
var pagenr = 0; // Model.PageNumber;
var itemsnr = 2; // Model.ItemsNumber;
var order = "Asc"; // Model.Order;
var method = "Official"; // Model.Method;
var amount = 2; //Model.AmountPage;
var itemsPerPage = [1, 2, 5, 10]; //Model.ItemsPerPage
var orderPage = ["Asc", "Des"]; //Model.OrderPage


/*
    Javascript Functions
*/
var page = {

    createLink: function (text, method, pagenr, itemsnr, order, id) {
        var anchor = document.createElement("a");
        var newurl = "/CUF/" + method + "/Page?pagenr=" + pagenr + "&itemsnr=" + itemsnr + "&order=" + order;
        anchor.setAttribute("href", newurl);
        var labelWithId = page.createLabelTextWithId(text, id);
        anchor.appendChild(labelWithId);
        //anchor.appendChild(document.createTextNode(name));
        return anchor;
    },

    createLabelText: function (text) {
        var label = document.createElement("label");
        label.appendChild(document.createTextNode(text));
        return label;
    },

    createLabelTextWithId: function (text, id) {
        var label = document.createElement("label");
        label.setAttribute("id", id);
        label.appendChild(document.createTextNode(text));
        return label;
    }
};


/*
    JQuery
*/
$(function () {

    /*
        Clean
    */
    $("#ItemsOrderSpan").empty();
    $("#ItemsNbrSpan").empty();
    //$("#CufListDiv").empty();
    $("#PageNavigationDiv").empty();


    /*
        Order
    */
    $("#ItemsOrderSpan").append(page.createLabelText("Order "));
    for (var o = 0; o < orderPage.length; ++o) {
        var id = "order" + orderPage[o];
        $("#ItemsOrderSpan").append(page.createLabelText("["));
        $("#ItemsOrderSpan").append(page.createLink(orderPage[o], method, pagenr, itemsnr, orderPage[o], id));
        $("#ItemsOrderSpan").append(page.createLabelText("]"));
        $(id).click(function (e) {
            //e.preventDefault();
            order = orderPage[o];
        }
        );
    }

    /*
        Page
    */
    $("#ItemsNbrSpan").append(page.createLabelText("Page "));
    for (var p = 0; p < itemsPerPage.length; ++p) {
        var id = "page" + itemsPerPage[p];
        $("#ItemsNbrSpan").append(page.createLabelText("["));
        $("#ItemsNbrSpan").append(page.createLink(itemsPerPage[p], method, pagenr, itemsPerPage[p], order, id));
        $("#ItemsNbrSpan").append(page.createLabelText("]"));
        $(id).click(function (e) {
            //e.preventDefault();
            itemsnr = itemsPerPage[p];
        }
        );
    }
    $("#ItemsNbrSpan").append(page.createLabelText("["));
    $("#ItemsNbrSpan").append(page.createLink("None", method, pagenr, itemsnr, order, "pageNone"));
    $("#pageNone").click(function (e) {
		//clear the Navigation DIV, so we could perform our custom display
		$("#PageNavigationDiv").empty();

		//create new Page View
		$("#PageNavigationDiv").append("<div id='WatchMoreBotton'><label>Watch More</label></div>");
		//'Button' Definition
		$("#WatchMoreBotton")
		.css('height', '50px').css('width', '400px')
		.css("border", "2px solid red").css("text-align", "center")
		.css("display", " table-cell").css("vertical-align", "middle")
		;
		//Button Function
		$("#WatchMoreBotton")
		.click(function (e) {
		    e.preventDefault();
			for (var t = 0; t < itemsnr; t++) {
				$('#CufList').append("<li><a href='/CUF/Official/Detail/PE'>Probabilidade e Estatistica Martelada</a></li>");
			}
			//Button scroll to bottom.
			$('#CufListDiv').animate({ scrollTop: $('.span4').innerHeight() }, 1000);
		});
		//Cuf List CSS
		$("#CufListDiv")
		.css('height', '300px').css('width', '400px')
		.css('overflow', 'auto').css('border', '1px solid gray');
		}
	);
	$("#ItemsNbrSpan").append(page.createLabelText("]"));


	/*
	    Navigation
	*/
    //    if (Model.PageNumber > 0)
    //    {
    $("#PageNavigationDiv").append(page.createLabelText("["));
    $("#PageNavigationDiv").append(page.createLink("First", method, 0, itemsnr, order, "idx" + "First"));
    //[@Html.ActionLink("First", Model.Method, new { pagenr = 0, itemsnr = Model.ItemsNumber, order = Model.Order })]
    $("#PageNavigationDiv").append(page.createLabelText("]"));
    $("#PageNavigationDiv").append(page.createLabelText("["));
    $("#PageNavigationDiv").append(page.createLink("Previous", method, pagenr - 1, itemsnr, order, "idx" + "Previous"));
    //[@Html.ActionLink("Previous", Model.Method, new { pagenr = (Model.PageNumber - 1), itemsnr = Model.ItemsNumber, order = Model.Order })]
    $("#PageNavigationDiv").append(page.createLabelText("]"));
    //    }
    var i = 0;
    //    for(var i = 0 ; i < Model.AmountPage; ++i){
    //        if (i == Model.PageNumber)
    //        {
    $("#PageNavigationDiv").append(page.createLabelText("[" + i + "]"));
    //[@i]
    //        }
    //        else 
    //        {
    $("#PageNavigationDiv").append(page.createLabelText("["));
    $("#PageNavigationDiv").append(page.createLink(i, method, i, itemsnr, order, "idx" + i));
    //[@Html.ActionLink("" + i, Model.Method, new { pagenr = i, itemsnr = Model.ItemsNumber, order = Model.Order })] 
    $("#PageNavigationDiv").append(page.createLabelText("]"));
    //        }
    //    }    
    //    if (!Model.LastPage)
    //    {
    $("#PageNavigationDiv").append(page.createLabelText("["));
    $("#PageNavigationDiv").append(page.createLink("Next", method, pagenr + 1, itemsnr, order, "idx" + "Next"));
    //[@Html.ActionLink("Next", Model.Method, new { pagenr = (Model.PageNumber + 1), itemsnr = Model.ItemsNumber, order = Model.Order })]
    $("#PageNavigationDiv").append(page.createLabelText("]"));
    $("#PageNavigationDiv").append(page.createLabelText("["));
    $("#PageNavigationDiv").append(page.createLink("Last", method, amount - 1, itemsnr, order, "idx" + "Last"));
    //[@Html.ActionLink("Last", Model.Method, new { pagenr = (Model.AmountPage - 1), itemsnr = Model.ItemsNumber, order = Model.Order })]
    $("#PageNavigationDiv").append(page.createLabelText("]"));
    //    }


});