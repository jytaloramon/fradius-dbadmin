FROM node:alpine3.18

# User and Group ID
ARG PUID=1024
ARG PGID=1024


RUN deluser node && \
    addgroup -g ${PGID} app && \
    adduser -u ${PUID} -D \
    -g "User for applications" \
    -G app \
    -s /sbin/nologin \
    app

WORKDIR /app

USER app


VOLUME [ "/app" ]

EXPOSE 80
