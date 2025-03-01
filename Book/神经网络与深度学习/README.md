# README

```bash
# 系统环境
alias mygitpush='git add --all && git commit -a -m $(TZ=UTC-8 date +"%Y%m%d-%H%M%S") && git push'
# apt-get install -y python3-full python3-pip  # 这本书使用的是Python2.7
# 这里使用Docker提供的Python2.7
# https://docs.docker.com/engine/install/debian/
for pkg in docker.io docker-doc docker-compose podman-docker containerd runc; do apt-get remove $pkg; done
apt-get update -y && apt-get install -y ca-certificates curl
install -m 0755 -d /etc/apt/keyrings
curl -fsSL https://download.docker.com/linux/debian/gpg -o /etc/apt/keyrings/docker.asc
chmod a+r /etc/apt/keyrings/docker.asc
echo "deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.asc] https://download.docker.com/linux/debian $(. /etc/os-release && echo "$VERSION_CODENAME") stable" | \
    tee /etc/apt/sources.list.d/docker.list > /dev/null
apt-get update -y
apt-get install -y docker-ce docker-ce-cli containerd.io docker-buildx-plugin docker-compose-plugin
systemctl enable docker.service
systemctl start docker.service
systemctl status docker.service
# docker run hello-world
# docker build --build-arg HTTP_PROXY="http://192.168.0.9:1991" .
# docker run --env HTTP_PROXY="http://192.168.0.9:1991" hello-world

# 图书
<http://neuralnetworksanddeeplearning.com/>
<https://github.com/mnielsen/neural-networks-and-deep-learning>
<https://github.com/tigerneil/neural-networks-and-deep-learning-zh-cn>
[Neural Network and Deep Learning.pdf](./assets/file/Neural%20Network%20and%20Deep%20Learning.pdf)

# 项目代码
mkdir -p /root/GithubProjects && cd /root/GithubProjects/
git clone git@github.com:michaelbrucelin/HelloAI.git
cd HelloAI/Book/神经网络与深度学习

# Python环境
cd /root/GithubProjects/HelloAI/Book/神经网络与深度学习/
docker build -t python2.7-jupyterlab .

# pip install numpy scipy matplotlib  # 需要更改Dockerfile并重新构建Docker镜像

# 运行Jupyter
# cd /root/GithubProjects/HelloAI/Book/神经网络与深度学习/
docker run -it --rm -p 8888:8888 -v /root/GithubProjects/HelloAI/Book/神经网络与深度学习:/workspace python2.7-jupyterlab

# 使用包
import numpy as np
```
