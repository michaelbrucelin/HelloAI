# README

```bash
# 系统环境
alias mygitpush='git add --all && git commit -a -m $(TZ=UTC-8 date +"%Y%m%d-%H%M%S") && git push'
apt-get install -y python3-full python3-pip

# 图书
[深度学习入门：基于Python的理论与实现.pdf](./assets/file/深度学习入门：基于Python的理论与实现.pdf)

# 项目代码
mkdir -p /root/GithubProjects && cd /root/GithubProjects/
git clone git@github.com:michaelbrucelin/HelloAI.git
cd HelloAI/Book/深度学习入门

# Python环境
cd /root/GithubProjects/HelloAI/Book/深度学习入门/
python3 -m venv venv
venv/bin/pip3 install jupyterlab
venv/bin/jupyter lab --allow-root --no-browser --port 8888 --ip=192.168.91.12

venv/bin/pip3 install numpy
venv/bin/pip3 install matplotlib

# 运行Jupyter
cd /root/GithubProjects/HelloAI/Book/深度学习入门/
venv/bin/jupyter lab --allow-root --no-browser --port 8888 --ip=192.168.91.12

# 使用包
import numpy as np
import matplotlib.pyplot as plt
%matplotlib inline
```
