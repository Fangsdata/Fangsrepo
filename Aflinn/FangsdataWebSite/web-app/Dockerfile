FROM node:13

LABEL maintainer="Stefán Örn Hrafnsson stefanorn92@gmail.com"

ENV NODE_ENV production
ENV PORT 3000
ENV WEBAPI http://172.23.0.2:5000/api

COPY . /app
WORKDIR /app
RUN npm install

ENTRYPOINT ["npm", "start"]
