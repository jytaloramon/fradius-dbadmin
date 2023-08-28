FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine3.17-amd64 as base

ARG PUID=1024
ARG PGID=1024


WORKDIR /app

RUN addgroup -g ${PGID} app \
    && adduser -G app -u ${PUID} -g "User for applications" -s /bin/sh -D app

VOLUME [ "/app" ]



FROM base as dev

RUN apk update \
    && apk add --no-cache nano \
    && apk add --no-cache openssh \
    && ssh-keygen -A \
    && sed -e 's/^#\?PermitRootLogin.*/PermitRootLogin yes/' -i /etc/ssh/sshd_config \
    && sed -e 's/^#\?PasswordAuthentication.*/PasswordAuthentication yes/' -i /etc/ssh/sshd_config \
    && sed -e 's/^#\?PermitEmptyPasswords.*/PermitEmptyPasswords yes/' -i /etc/ssh/sshd_config \
    && passwd -d root \
    && passwd -d app \
    && printf "#!/bin/sh\n/usr/sbin/sshd & /bin/sh\n" > /startup.sh \
    && chmod +x /startup.sh


EXPOSE 22 5046

CMD [ "/startup.sh" ]
