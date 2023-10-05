# Makefile for Rgg Calculator
# simple Makefile (no need to use special variables)

.PHONY: all pack clean test doc run profile help pkg

REPOFILES=.git .gitignore .editorconfig  debugging.png dokumentace.pdf hodnoceni.txt mockup plan profiling README.md screenshot.png skutecnost.txt src installer

# Compile sources (Python doesn't need)
all:
	 @echo "Python is interpreted language, everything is ready"

# Pack repo, doc and package
pack: clean doc 
	mkdir ../xvever13_xsedmi04_xwagne10_xplach09
	mkdir ../xvever13_xsedmi04_xwagne10_xplach09/repo
	cd .. && cp -rp $(REPOFILES) xvever13_xsedmi04_xwagne10_xplach09/repo
	cp -r ../doc ../xvever13_xsedmi04_xwagne10_xplach09
	mkdir ../xvever13_xsedmi04_xwagne10_xplach09/install
	cp ../installer/*.deb  ../xvever13_xsedmi04_xwagne10_xplach09/install
	cd .. && zip -r xvever13_xsedmi04_xwagne10_xplach09/xvever13_xsedmi04_xwagne10_xplach09.zip xvever13_xsedmi04_xwagne10_xplach09

# Clean repo of uneeded files
clean:
	rm -rf ../doc
	rm -rf __pycache__
	rm -rf ../.vscode
	rm -rf ../xvever13_xsedmi04_xwagne10_xplach09

# Run tests
test: tests_rgg_mathlib.py rgg_mathlib.py
	python tests_rgg_mathlib.py

# Create doxygen documentation
doc: Doxyfile calculator_gui.py expression_evaluator.py rgg_mathlib.py calc.py tests_rgg_mathlib.py
	doxygen

# Run calculator
run: calculator_gui.py expression_evaluator.py rgg_mathlib.py calc.py
	python calc.py

# Run profiling
profile: profiling.py deviation.py numbers rgg_mathlib.py
	python profiling.py numbers

# Info about program
help:
	@echo "This program uses Python, make sure Python is installed"
	@echo "At least Python3 is required"

# One time command to generate pacakge
pkg:
	cd ../installer/rggcalc-1.0 && dpkg-buildpackage -rfakeroot -uc -b
