FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine3.17-amd64

ARG PUID=1024
ARG PGID=1024

RUN apk update \
    && apk add --no-cache nano \
    && apk add --no-cache openssh \
    && ssh-keygen -A

RUN sed -e 's/^#\?PermitRootLogin.*/PermitRootLogin yes/' -i /etc/ssh/sshd_config \
    && sed -e 's/^#\?PasswordAuthentication.*/PasswordAuthentication yes/' -i /etc/ssh/sshd_config \
    && sed -e 's/^#\?PermitEmptyPasswords.*/PermitEmptyPasswords yes/' -i /etc/ssh/sshd_config

RUN addgroup -g ${PGID} fradius \
    && adduser -G fradius -s /bin/sh -u ${PUID} -D fradius

RUN passwd -d root \
    && passwd -d fradius 

RUN printf "#!/bin/sh\n/usr/sbin/sshd & /bin/sh\n" > /startup.sh \
    && chmod +x /startup.sh

EXPOSE 22

VOLUME [ "/app" ]

CMD [ "/startup.sh" ]
