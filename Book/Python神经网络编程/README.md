# README

```bash
alias mygitpush='git add --all && git commit -a -m $(TZ=UTC-8 date +"%Y%m%d-%H%M%S") && git push'
apt-get install -y python3-full python3-pip

mkdir -p /root/GithubProjects && cd /root/GithubProjects/
git clone git@github.com:michaelbrucelin/HelloAI.git
cd HelloAI/Book/Python神经网络编程

python3 -m venv venv
venv/bin/pip3 install jupyterlab
venv/bin/jupyter lab --allow-root --no-browser --port 8888 --ip=192.168.91.12
```
