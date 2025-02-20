$(document).ready(function() {
    // Time series chart
    $.get('/Incidents/GetIncidentStats', function(data) {
        var dates = data.map(x => x.date);
        var counts = data.map(x => x.count);
        var injuries = data.map(x => x.injuries);
        var fatalities = data.map(x => x.fatalities);

        var traces = [
            {
                x: dates,
                y: counts,
                name: 'Incidents',
                type: 'scatter'
            },
            {
                x: dates,
                y: injuries,
                name: 'Injuries',
                type: 'scatter',
                yaxis: 'y2'
            },
            {
                x: dates,
                y: fatalities,
                name: 'Fatalities',
                type: 'scatter',
                yaxis: 'y2'
            }
        ];

        var layout = {
            title: 'Incident Statistics Over Time',
            height: 400,
            yaxis: { title: 'Number of Incidents' },
            yaxis2: {
                title: 'Injuries/Fatalities',
                overlaying: 'y',
                side: 'right'
            }
        };

        Plotly.newPlot('incidentChart', traces, layout);
    });

    // Incident types pie chart
    $.get('/Incidents/GetIncidentsByType', function(data) {
        var trace = {
            labels: data.map(x => x.type),
            values: data.map(x => x.count),
            type: 'pie',
            hole: 0.4
        };

        var layout = {
            title: 'Incidents Type',
            height: 400,
            showlegend: true
        };

        Plotly.newPlot('incidentTypeChart', [trace], layout);
    });

    // State-wise incidents map
    $.get('/Incidents/GetIncidentsByState', function(data) {
        var trace = {
            type: 'choropleth',
            locationmode: 'USA-states',
            locations: data.map(x => x.state),
            z: data.map(x => x.count),
            text: data.map(x => `${x.state}: ${x.count} incidents`),
            colorscale: 'Reds',
            colorbar: {
                title: 'Number of Incidents'
            }
        };

        var layout = {
            title: 'Incidents by State',
            height: 400,
            geo: {
                scope: 'usa',
                showlakes: true,
                lakecolor: 'rgb(255,255,255)'
            }
        };

        Plotly.newPlot('stateMapChart', [trace], layout);
    });
});