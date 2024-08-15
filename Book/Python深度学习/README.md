# README

```bash
# 系统环境
alias mygitpush='git add --all && git commit -a -m $(TZ=UTC-8 date +"%Y%m%d-%H%M%S") && git push'
apt-get install -y python3-full python3-pip

# 图书
[Python神经网络编程.pdf](./assets/file/Python深度学习.pdf)

# 项目代码
mkdir -p /root/GithubProjects && cd /root/GithubProjects/
git clone git@github.com:michaelbrucelin/HelloAI.git
cd HelloAI/Book/Python深度学习

# Python环境
cd /root/GithubProjects/HelloAI/Book/Python深度学习/
python3 -m venv venv
venv/bin/pip3 install jupyterlab
venv/bin/jupyter lab --allow-root --no-browser --port 8888 --ip=192.168.91.12

venv/bin/pip3 install tensorflow  # 会自动安装keras
# venv/bin/pip3 install numpy
# venv/bin/pip3 install scipy
# venv/bin/pip3 install matplotlib

# 运行Jupyter
cd /root/GithubProjects/HelloAI/Book/Python深度学习/
venv/bin/jupyter lab --allow-root --no-browser --port 8888 --ip=192.168.91.12

# 使用包
# import numpy as np
# import scipy.special
# import matplotlib.pyplot as plt
# %matplotlib inline
```
