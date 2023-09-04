FROM node:20.5.1-alpine3.18 as prod

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


# Develop container
FROM node:20.5.1-bookworm as dev

ARG PUID=1024
ARG PGID=1024

WORKDIR /app

RUN apt-get update \
    && apt-get install -y nano git openssh-server openssl \
    && mkdir /run/sshd \
    && sed -e 's/^#\?PermitRootLogin.*/PermitRootLogin yes/' -i /etc/ssh/sshd_config \
    && sed -e 's/^#\?PasswordAuthentication.*/PasswordAuthentication yes/' -i /etc/ssh/sshd_config \
    && sed -e 's/^#\?PermitEmptyPasswords.*/PermitEmptyPasswords yes/' -i /etc/ssh/sshd_config \
    && userdel node \
    && groupadd --gid ${PGID} app \
    && useradd --uid ${PUID} --gid app -m -c "User for applications" -s /bin/bash app \
    && passwd -d root \
    && passwd -d app 


EXPOSE 22 80

VOLUME [ "/app" ]

CMD ["/usr/sbin/sshd", "-D"]
