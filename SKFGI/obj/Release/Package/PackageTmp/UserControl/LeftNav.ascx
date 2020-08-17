<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftNav.ascx.cs" Inherits="CollegeERP.UserControl.LeftNav" %>
<h6 id="h-menu-products" class="selected"><a href="#products"><span>Products</span></a></h6>
				    <ul id="menu-products" class="opened">
						<li><a href="">Manage Products</a></li>
						<li class="selected"><a href="">Add Product</a></li>
						<li class="collapsible">
							<a href="#" class="collapsible plus">Sales</a>
							<ul id="whatever" class="collapsed">
								<li><a href="">Today</a></li>
								<li><a href="">Yesterday</a></li>
								<li class="collapsible last">
									<a href="#" class="collapsible plus">Archive</a>
									<ul class="collapsed">
										<li><a href="">Last Week</a></li>
										<li><a href="">Last Month</a></li>
									</ul>
								</li>
							</ul>
						</li>
						<li class="collapsible last">
							<a href="#" class="collapsible plus">Offers</a>
							<ul class="collapsed">
								<li><a href="">Coupon Codes</a></li>
								<li class="last"><a href="">Rebates</a></li>
							</ul>
						</li>
					</ul>

				    <h6 id="h-menu-pages" class="selected"><a href="#pages"><span>Pages</span></a></h6>
					<ul id="menu-pages" class="opened">
						<li><a href="">Manage Pages</a></li>
						<li><a href="">New Page</a></li>
						<li class="collapsible last">
							<a href="#" class="plus">Help</a>
							<ul class="collapsed">
								<li><a href="">How to Add a New Page</a></li>
								<li class="last"><a href="">How to Add a New Page</a></li>
							</ul>
						</li>
					</ul>
					<h6 id="h-menu-events" class="selected"><a href="#events"><span>Events</span></a></h6>
					<ul id="menu-events" class="opened">
						<li><a href="">Manage Events</a></li>
						<li class="last"><a href="">New Event</a></li>
					</ul>
					<h6 id="h-menu-links" class="selected"><a href="#links"><span>Links</span></a></h6>
					<ul id="menu-links" class="opened">
						<li><a href="">Manage Links</a></li>
						<li class="last"><a href="">Add Link</a></li>
					</ul>
					<h6 id="h-menu-settings" class="selected"><a href="#settings"><span>Settings</span></a></h6>
					<ul id="menu-settings" class="opened">
						<li><a href="">Manage Settings</a></li>
						<li class="last"><a href="">New Setting</a></li>
					</ul>