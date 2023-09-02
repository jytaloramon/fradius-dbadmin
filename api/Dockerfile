FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine3.17-amd64 as prod

ARG PUID=1024
ARG PGID=1024

WORKDIR /app

RUN addgroup -g ${PGID} app \
    && adduser -G app -u ${PUID} -g "User for applications" -s /bin/sh -D app

VOLUME [ "/app" ]


# Develop container
FROM mcr.microsoft.com/dotnet/sdk:7.0.400-bookworm-slim as dev

ARG PUID=1024
ARG PGID=1024

WORKDIR /app

RUN apt-get update \
    && apt-get install -y nano openssh-server openssl \
    && mkdir /run/sshd \
    && sed -e 's/^#\?PermitRootLogin.*/PermitRootLogin yes/' -i /etc/ssh/sshd_config \
    && sed -e 's/^#\?PasswordAuthentication.*/PasswordAuthentication yes/' -i /etc/ssh/sshd_config \
    && sed -e 's/^#\?PermitEmptyPasswords.*/PermitEmptyPasswords yes/' -i /etc/ssh/sshd_config \
    && groupadd --gid ${PGID} app \
    && useradd --uid ${PUID} --gid app -m -c "User for applications" -s /bin/bash app \
    && passwd -d root \
    && passwd -d app 


EXPOSE 22 5046

VOLUME [ "/app" ]

CMD ["/usr/sbin/sshd", "-D"]
