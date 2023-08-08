# FRadius-DBAdmin - Front-End (WEB)

## Dev

### Build Image

```bash
$ docker build --build-arg PGID=${GROUP_ID} --build-arg PUID=${USER_ID} -t fradius-frontend:v0.1 ./
```

### Use Container

```bash
$ docker run --name ${CONTAINER_NAME} -v $(pwd)/path:/app -it fradius-frontend:v0.1 /bin/sh
```