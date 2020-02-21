cd Aflinn/OffloadWebApi/
heroku container:push web -a fangsdata-api ;
heroku container:release web -a fangsdata-api;

cd ../FangsdataWebSite/web-app/ 
heroku container:push web -a fangsdata;
heroku container:release web -a fangsdata;