google.maps.event.addDomListener(window, 'load', addMap());
        
            function addMap() {
                var mapOptions = {

                    zoom: 15,

                    center: new google.maps.LatLng(46.743119, 7.629385),

                    // How you would like to style the map. 
                    // This is where you would paste any style found on Snazzy Maps.
                    styles: [{"featureType":"administrative","elementType":"labels.text.fill","stylers":[{"color":"#444444"}]},{"featureType":"landscape","elementType":"all","stylers":[{"color":"#f2f2f2"}]},{"featureType":"landscape","elementType":"geometry.fill","stylers":[{"color":"#ffffff"}]},{"featureType":"poi","elementType":"all","stylers":[{"visibility":"off"}]},{"featureType":"road","elementType":"all","stylers":[{"saturation":-100},{"lightness":45}]},{"featureType":"road","elementType":"geometry.stroke","stylers":[{"visibility":"off"}]},{"featureType":"road","elementType":"labels.text.fill","stylers":[{"color":"#565656"}]},{"featureType":"road","elementType":"labels.text.stroke","stylers":[{"color":"#ff0000"},{"visibility":"off"}]},{"featureType":"road.highway","elementType":"all","stylers":[{"visibility":"simplified"}]},{"featureType":"road.highway","elementType":"geometry.fill","stylers":[{"color":"#c2a779"}]},{"featureType":"road.arterial","elementType":"geometry.fill","stylers":[{"color":"#c2a779"}]},{"featureType":"road.arterial","elementType":"labels.icon","stylers":[{"visibility":"off"}]},{"featureType":"road.local","elementType":"geometry.fill","stylers":[{"color":"#c2a779"}]},{"featureType":"transit","elementType":"all","stylers":[{"visibility":"off"}]},{"featureType":"transit.line","elementType":"geometry.fill","stylers":[{"visibility":"on"},{"color":"#8f8f8f"}]},{"featureType":"transit.station.bus","elementType":"all","stylers":[{"visibility":"on"}]},{"featureType":"transit.station.bus","elementType":"labels.icon","stylers":[{"saturation":"-100"},{"lightness":"0"}]},{"featureType":"transit.station.rail","elementType":"labels.icon","stylers":[{"visibility":"on"},{"saturation":"-100"}]},{"featureType":"water","elementType":"all","stylers":[{"color":"#949494"},{"visibility":"on"}]},{"featureType":"water","elementType":"geometry.fill","stylers":[{"color":"#adadad"}]},{"featureType":"water","elementType":"labels.text.fill","stylers":[{"color":"#565656"}]}]
                };

                // Get the HTML DOM element that will contain your map 
                // We are using a div with id="map" seen below in the <body>
                var mapElement = document.getElementById('map');

                // Create the Google Map using our element and options defined above
                var map = new google.maps.Map(mapElement, mapOptions);

                // Let's also add a marker while we're at it
                /*var marker = new google.maps.Marker({
                    position: new google.maps.LatLng(40.6700, -73.9400),
                    map: map,
                    title: 'Snazzy!'
                });*/
                
                var markerOptions = {
					map: map,
					position: {
						lat: 46.743119,
						lng: 7.629385
					}
				};
				
				markerOptions.icon = {
					path: 'M11 2c-3.9 0-7 3.1-7 7 0 5.3 7 13 7 13 0 0 7-7.7 7-13 0-3.9-3.1-7-7-7Zm0 9.5c-1.4 0-2.5-1.1-2.5-2.5 0-1.4 1.1-2.5 2.5-2.5 1.4 0 2.5 1.1 2.5 2.5 0 1.4-1.1 2.5-2.5 2.5Z',
					scale: 1.6363636363636363636363636364,
					anchor: new google.maps.Point(11, 22),
					fillOpacity: 1,
					fillColor: '#e53946',
					strokeOpacity: 0
				};
				
				var marker = new google.maps.Marker(markerOptions);
            }